using System;
using WeatherForecast.Endpoint.Models;

namespace WeatherForecast.Endpoint.Mapping
{
    public static class ForecastWebApiMapping
    {
        public static ForecastDto ToDto(this Domain.Forecast obj)
        {
            return new ForecastDto(
                obj.Temperature.ToDto(),
                obj.Wind.ToDto(),
                obj.Pressure,
                obj.Humidity,
                obj.GeomagneticActivity,
                obj.WaterTemperature,
                obj.WeatherType,
                obj.City,
                obj.Date);
        }

        private static TemperatureDto ToDto(this Domain.Temperature obj)
            => new TemperatureDto(obj.Value, obj.FeelsLike);

        private static WindDto ToDto(this Domain.Wind obj)
        {
            var direction = obj.Direction switch
            {
                 Domain.Wind.WindDirection.North => WindDto.WindDirectionDto.North,
                 Domain.Wind.WindDirection.West => WindDto.WindDirectionDto.West,
                 Domain.Wind.WindDirection.South => WindDto.WindDirectionDto.South,
                 Domain.Wind.WindDirection.East => WindDto.WindDirectionDto.East,
                 Domain.Wind.WindDirection.NorthEast => WindDto.WindDirectionDto.NorthEast,
                 Domain.Wind.WindDirection.NorthWest => WindDto.WindDirectionDto.NorthWest,
                 Domain.Wind.WindDirection.SouthEast => WindDto.WindDirectionDto.SouthEast,
                 Domain.Wind.WindDirection.SouthWest => WindDto.WindDirectionDto.SouthWest,
                 Domain.Wind.WindDirection.Calm=> WindDto.WindDirectionDto.Calm,
                 _ => throw new ArgumentOutOfRangeException()
            };

            return new WindDto(obj.Value, direction);
        }
    }
}