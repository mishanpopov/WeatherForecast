using System;
using Google.Protobuf.WellKnownTypes;

namespace WeatherForecast.GismeteoRipper.Models
{
    public static class Mappers
    {
        public static WeatherForecast.Forecast ToProto(this Forecast obj)
        {
            return new WeatherForecast.Forecast
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

        private static WeatherForecast.Temperature ToProto(this Temperature obj)
        {
            return new WeatherForecast.Temperature
            {
                Value = obj.Value,
                FeelsLike = obj.FeelsLike
            };
        }

        private static WeatherForecast.Wind ToProto(this Wind obj)
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

            return new WeatherForecast.Wind
            {
                Value = obj.Value,
                Direction = direction
            };
        }
    }
}