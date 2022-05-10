using System;
using WeatherForecast.Domain;

namespace WeatherForecast.Endpoint.Mapping
{
    public static class ForecastProtoMapping
    {
        public static Forecast ToDomain(this ForecastWeather.WeatherForecast obj)
        {
            return new Forecast(
                obj.Temperature.ToDomain(),
                obj.Wind.ToDomain(),
                obj.Pressure,
                obj.Humidity,
                obj.GeomagneticActivity,
                obj.WaterTemperature,
                obj.WeatherType,
                obj.City,
                obj.Date.ToDateTime());
        }

        public static Temperature ToDomain(this ForecastWeather.Temperature obj)
            => new Temperature(obj.Value, obj.FeelsLike);

        public static Wind ToDomain(this ForecastWeather.Wind obj)
        {
            var direction = obj.Direction switch
            {
                ForecastWeather.WindDirection.North => Wind.WindDirection.North,
                ForecastWeather.WindDirection.West => Wind.WindDirection.West,
                ForecastWeather.WindDirection.South => Wind.WindDirection.South,
                ForecastWeather.WindDirection.East => Wind.WindDirection.East,
                ForecastWeather.WindDirection.SouthEast => Wind.WindDirection.SouthEast,
                ForecastWeather.WindDirection.SouthWest => Wind.WindDirection.SouthWest,
                ForecastWeather.WindDirection.NorthEast => Wind.WindDirection.NorthEast,
                ForecastWeather.WindDirection.NorthWest => Wind.WindDirection.NorthWest,
                ForecastWeather.WindDirection.Calm => Wind.WindDirection.Calm,
                _ => throw new ArgumentOutOfRangeException()
            };

            return new Wind(obj.Value, direction);
        }
    }
}