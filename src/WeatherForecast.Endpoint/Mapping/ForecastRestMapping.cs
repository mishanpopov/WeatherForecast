using WeatherForecast.Domain;
using WeatherForecast.Endpoint.Models;
using Temperature = WeatherForecast.Domain.Temperature;
using Wind = WeatherForecast.Domain.Wind;

namespace WeatherForecast.Endpoint.Mapping
{
    public static class ForecastRestMapping
    {
        public static ForecastDto ToDto(this Forecast obj)
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

        private static TemperatureDto ToDto(this Temperature obj)
            => new TemperatureDto(obj.Value, obj.FeelsLike);

        private static WindDto ToDto(this Wind obj)
        {
            var direction = obj.Direction switch
            {
                 Wind.WindDirection.North => WindDto.WindDirectionDto.North,
                 Wind.WindDirection.West => WindDto.WindDirectionDto.West,
                 Wind.WindDirection.South => WindDto.WindDirectionDto.South,
                 Wind.WindDirection.East => WindDto.WindDirectionDto.East,
                 Wind.WindDirection.NorthEast => WindDto.WindDirectionDto.NorthEast,
                 Wind.WindDirection.NorthWest => WindDto.WindDirectionDto.NorthWest,
                 Wind.WindDirection.SouthEast => WindDto.WindDirectionDto.SouthEast,
                 Wind.WindDirection.SouthWest => WindDto.WindDirectionDto.SouthWest,
                 Wind.WindDirection.Calm=> WindDto.WindDirectionDto.Calm
            };

            return new WindDto(obj.Value, direction);
        }
    }
}