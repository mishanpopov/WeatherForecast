using System;
using ForecastWeather;
using Google.Protobuf.WellKnownTypes;
using WeatherForecast.GismeteoRipper.Models;
using Temperature = WeatherForecast.GismeteoRipper.Models.Temperature;
using WeatherForecast = ForecastWeather.WeatherForecast;
using Wind = WeatherForecast.GismeteoRipper.Models.Wind;

namespace FuuuWeatherForecast.GismeteoRipper.Models
{
    public static class Mappers
    {
        public static ForecastWeather.WeatherForecast ToProto(this Forecast obj)
        {
            return new ForecastWeather.WeatherForecast
            {
                Temperature = obj.Temperature.ToProto(),
                Date = obj.Date.ToTimestamp(),
                GeomagneticActivity = obj.GeomagneticActivity,
                Humidity = obj.Humidity,
                Pressure = obj.Pressure,
                WaterTemperature = obj.WaterTemperature,
                Wind = obj.Wind.ToProto(),
                WeatherType = obj.WeatherType,
                City = obj.City
            };
        }

        private static ForecastWeather.Temperature ToProto(this Temperature obj)
        {
            return new ForecastWeather.Temperature
            {
                Value = obj.Value,
                FeelsLike = obj.FeelsLike
            };
        }

        private static ForecastWeather.Wind ToProto(this Wind obj)
        {
            var direction = obj.Direction switch
            {
                Wind.WindDirection.North => WindDirection.North,
                Wind.WindDirection.West => WindDirection.West,
                Wind.WindDirection.East => WindDirection.East,
                Wind.WindDirection.South => WindDirection.South,
                Wind.WindDirection.NorthWest => WindDirection.NorthWest,
                Wind.WindDirection.NorthEast => WindDirection.NorthEast,
                Wind.WindDirection.SouthWest => WindDirection.SouthWest,
                Wind.WindDirection.SouthEast => WindDirection.SouthEast,
                Wind.WindDirection.Calm => WindDirection.Calm,
                _ => throw new ArgumentOutOfRangeException()
            };

            return new ForecastWeather.Wind
            {
                Value = obj.Value,
                Direction = direction
            };
        }
    }
}