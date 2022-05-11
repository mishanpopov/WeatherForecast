using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using WeatherForecast.GismeteoRipper.Models;

namespace WeatherForecast.GismeteoRipper
{
    public class Parser : IParser
    {
        private static readonly NumberFormatInfo DecimalNumberFormatInfo =
            new NumberFormatInfo
            {
                NumberDecimalSeparator = ",",
                NegativeSign = "\u002d",
                NumberNegativePattern = 1
            };


        public IEnumerable<City> GetPopularCities(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var cityNodes = htmlDoc.DocumentNode
                .SelectNodes(".//div[@class='cities-popular']//div[@class='list-item']/a")
                .ToList();

            if (cityNodes == null)
            {
                throw new Exception("handle me");
            }

            var cities = cityNodes.Select(c =>
            {
                var name = c.InnerText;
                var id = c.GetAttributeValue("href", string.Empty).Trim('/');
                return new City(name, id);
            });

            return cities;
        }

        public WeatherForecast.GismeteoRipper.Models.Forecast GetForecast(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var temperature = ParseTemperature(htmlDoc.DocumentNode);
            var weatherType = ParseWeatherType(htmlDoc.DocumentNode);
            var wind = ParseWind(htmlDoc.DocumentNode);
            var pressure = ParsePressure(htmlDoc.DocumentNode);
            var humidity = ParseHumidity(htmlDoc.DocumentNode);
            var geomagneticActivity = ParseGeomagneticActivity(htmlDoc.DocumentNode);
            var waterTemperature = ParseWaterTemperature(htmlDoc.DocumentNode);

            return new WeatherForecast.GismeteoRipper.Models.Forecast(
                temperature,
                wind,
                pressure,
                humidity,
                geomagneticActivity,
                waterTemperature,
                weatherType,
                DateTime.UtcNow);
        }

        private static WeatherForecast.GismeteoRipper.Models.Temperature ParseTemperature(HtmlNode htmlNode)
        {
            const string temperatureXPath =
                ".//div[@class='now']/div[@class='now-weather']/span[@class='unit unit_temperature_c']";

            const string feelsLikeTemperatureXPath =
                ".//div[@class='now']/div[@class='now-feel']/span[@class='unit unit_temperature_c']";

            var temperatureNode = htmlNode.SelectSingleNode(temperatureXPath);
            var temperature = ParseDouble(temperatureNode);

            var feelsLikeTemperatureNode = htmlNode.SelectSingleNode(feelsLikeTemperatureXPath);
            var feelsLikeTemperature = ParseDouble(feelsLikeTemperatureNode);

            return new WeatherForecast.GismeteoRipper.Models.Temperature(temperature, feelsLikeTemperature);
        }

        private static string ParseWeatherType(HtmlNode htmlNode)
        {
            const string xPath = ".//div[@class='now']/div[@class='now-desc']";

            var weatherTypeNode = htmlNode.SelectSingleNode(xPath);
            return weatherTypeNode.InnerText.Trim();
        }

        private static WeatherForecast.GismeteoRipper.Models.Wind ParseWind(HtmlNode htmlNode)
        {
            const string valueXPath =
                ".//div[@class='now']/div[@class='now-info']//div[@class='unit unit_wind_m_s']/text()";

            const string directionXPath =
                ".//div[@class='now']/div[@class='now-info']//div[@class='unit unit_wind_m_s']/div[@class='item-measure']/div[2]";

            var windValueNode = htmlNode.SelectSingleNode(valueXPath);
            var wind = ParseDouble(windValueNode);

            var windDirectionNode = htmlNode.SelectSingleNode(directionXPath);
            var direction = windDirectionNode.InnerText.Trim().ToLower() switch
            {
                "западный" => WeatherForecast.GismeteoRipper.Models.Wind.WindDirection.West,
                "сз"=>WeatherForecast.GismeteoRipper.Models.Wind.WindDirection.NorthWest,
                "св"=>WeatherForecast.GismeteoRipper.Models.Wind.WindDirection.NorthEast,
                "южный"=>WeatherForecast.GismeteoRipper.Models.Wind.WindDirection.South,
                "северный"=>WeatherForecast.GismeteoRipper.Models.Wind.WindDirection.North,
                "юз"=>WeatherForecast.GismeteoRipper.Models.Wind.WindDirection.SouthWest,
                "восточный"=>WeatherForecast.GismeteoRipper.Models.Wind.WindDirection.East,
                "юв"=>WeatherForecast.GismeteoRipper.Models.Wind.WindDirection.SouthEast,
                "штиль"=>WeatherForecast.GismeteoRipper.Models.Wind.WindDirection.Calm,
                _ => throw new ArgumentOutOfRangeException("wind direction")
            };

            return new WeatherForecast.GismeteoRipper.Models.Wind(wind, direction);
        }

        private static int ParsePressure(HtmlNode htmlNode)
        {
            const string xPath =
                ".//div[@class='now']/div[@class='now-info']//div[@class='unit unit_pressure_mm_hg_atm']/text()";

            var pressureValueNode = htmlNode.SelectSingleNode(xPath);
            return ParseInteger(pressureValueNode);
        }

        private static int ParseHumidity(HtmlNode htmlNode)
        {
            const string xPath
                = ".//div[@class='now']/div[@class='now-info']//div[@class='now-info-item humidity']/div[@class='item-value']";

            var humidityValueNode = htmlNode.SelectSingleNode(xPath);
            return ParseInteger(humidityValueNode);
        }

        private static int ParseGeomagneticActivity(HtmlNode htmlNode)
        {
            const string xPath
                = ".//div[@class='now']/div[@class='now-info']//div[@class='now-info-item gm']/div[@class='item-value']";

            var geomagneticActivityNode = htmlNode.SelectSingleNode(xPath);
            return ParseInteger(geomagneticActivityNode);
        }

        private static double ParseWaterTemperature(HtmlNode htmlNode)
        {
            const string xPath
                = ".//div[@class='now']/div[@class='now-info']//div[@class='now-info-item water']//div[@class='unit unit_temperature_c']";

            var waterTemperatureNode = htmlNode.SelectSingleNode(xPath);
            return ParseDouble(waterTemperatureNode);
        }

        private static double ParseDouble(HtmlNode htmlNode)
        {
            if (!double.TryParse(htmlNode.InnerText,
                NumberStyles.Any,
                DecimalNumberFormatInfo,
                out var value))
            {
                throw new Exception("handle me");
            }

            return value;
        }

        private static int ParseInteger(HtmlNode htmlNode)
        {
            if (!int.TryParse(htmlNode.InnerText,
                out var value))
            {
                throw new Exception("handle me");
            }

            return value;
        }
    }
}