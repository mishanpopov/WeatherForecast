using System;

namespace WeatherForecast.Endpoint.Mapping
{
    public static class ForecastProtoMapping
    {
        public static Domain.Forecast ToDomain(this Forecast obj)
        {
            return new Domain.Forecast(
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

        public static Domain.Temperature ToDomain(this Temperature obj)
            => new Domain.Temperature(obj.Value, obj.FeelsLike);

        public static Domain.Wind ToDomain(this Wind obj)
        {
            var direction = obj.Direction switch
            {
                WindDirection.North => Domain.Wind.WindDirection.North,
                WindDirection.West => Domain.Wind.WindDirection.West,
                WindDirection.South => Domain.Wind.WindDirection.South,
                WindDirection.East => Domain.Wind.WindDirection.East,
                WindDirection.SouthEast => Domain.Wind.WindDirection.SouthEast,
                WindDirection.SouthWest => Domain.Wind.WindDirection.SouthWest,
                WindDirection.NorthEast => Domain.Wind.WindDirection.NorthEast,
                WindDirection.NorthWest => Domain.Wind.WindDirection.NorthWest,
                WindDirection.Calm => Domain.Wind.WindDirection.Calm,
                _ => throw new ArgumentOutOfRangeException()
            };

            return new Domain.Wind(obj.Value, direction);
        }
    }
}