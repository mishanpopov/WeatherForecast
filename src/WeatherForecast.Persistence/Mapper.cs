using System;
using WeatherForecast.Domain;

namespace WeatherForecast.Persistence
{
    public static class Mapper
    {
        public static ForecastDbo ToDbo(this Forecast obj)
        {
            return new ForecastDbo(
                obj.Temperature.ToDbo(),
                obj.Wind.ToDbo(),
                obj.Pressure,
                obj.Humidity,
                obj.GeomagneticActivity,
                obj.WaterTemperature,
                obj.WeatherType,
                obj.City.ToLower(),
                obj.Date);
        }

        private static TemperatureDbo ToDbo(this Temperature obj)
            => new TemperatureDbo(obj.Value, obj.FeelsLike);

        private static WindDbo ToDbo(this Wind obj)
        {
            var direction = obj.Direction switch
            {
                Wind.WindDirection.North => WindDbo.WindDirection.North,
                Wind.WindDirection.West => WindDbo.WindDirection.West,
                Wind.WindDirection.East => WindDbo.WindDirection.East,
                Wind.WindDirection.South => WindDbo.WindDirection.South,
                Wind.WindDirection.SouthEast => WindDbo.WindDirection.SouthEast,
                Wind.WindDirection.SouthWest => WindDbo.WindDirection.SouthWest,
                Wind.WindDirection.NorthEast => WindDbo.WindDirection.NorthEast,
                Wind.WindDirection.NorthWest => WindDbo.WindDirection.NorthWest,
                Wind.WindDirection.Calm => WindDbo.WindDirection.Calm,
                _=>throw new ArgumentOutOfRangeException()
            };

            return new WindDbo(obj.Value, direction);
        }

        public static Forecast ToDomain(this ForecastDbo obj)
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
                obj.Date);
        }
        
        public static Temperature ToDomain(this TemperatureDbo obj)
            => new Temperature(obj.Value, obj.FeelsLike);

        public static Wind ToDomain(this WindDbo obj)
        {
            var direction = obj.Direction switch
            {
                WindDbo.WindDirection.North => Wind.WindDirection.North,
                WindDbo.WindDirection.West => Wind.WindDirection.West,
                WindDbo.WindDirection.East => Wind.WindDirection.East,
                WindDbo.WindDirection.South => Wind.WindDirection.South,
                WindDbo.WindDirection.SouthEast => Wind.WindDirection.SouthEast,
                WindDbo.WindDirection.SouthWest => Wind.WindDirection.SouthWest,
                WindDbo.WindDirection.NorthEast => Wind.WindDirection.NorthEast,
                WindDbo.WindDirection.NorthWest => Wind.WindDirection.NorthWest,
                WindDbo.WindDirection.Calm => Wind.WindDirection.Calm,
                _=>throw new ArgumentOutOfRangeException()
            };

            return new Wind(obj.Value, direction);
        }
    }
}