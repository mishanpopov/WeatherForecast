using System;

namespace WeatherForecast.Endpoint.Models
{
    public class ForecastDto
    {
        public ForecastDto(
            TemperatureDto temperature,
            WindDto wind,
            int pressure,
            int humidity,
            int geomagneticActivity,
            double waterTemperature,
            string weatherType,
            string city,
            DateTime date)
        {
            Temperature = temperature;
            Wind = wind;
            Pressure = pressure;
            Humidity = humidity;
            GeomagneticActivity = geomagneticActivity;
            WaterTemperature = waterTemperature;
            WeatherType = weatherType;
            City = city;
            Date = date;
        }
        
        public TemperatureDto Temperature { get; }
        public WindDto Wind { get; }
        public int Pressure { get; }
        public int Humidity { get; }
        public int GeomagneticActivity { get; }
        public double WaterTemperature { get; }
        public string WeatherType { get; }
        public string City { get; }
        public DateTime Date { get; }
    }
    
    public class TemperatureDto
    {
        public TemperatureDto(double value, double feelsLike)
        {
            Value = value;
            FeelsLike = feelsLike;
        }

        public double Value { get; }
        public double FeelsLike { get; }
    }

    public class WindDto
    {
        public WindDto(double value, WindDirectionDto direction)
        {
            Value = value;
            Direction = direction;
        }

        public double Value { get; }
        public WindDirectionDto Direction { get; }

        public enum WindDirectionDto : short
        {
            Undefined = -1,
            North = 1,
            West = 2,
            East = 3,
            South = 4,
            SouthEast = 5,
            SouthWest = 6,
            NorthEast = 7,
            NorthWest = 8,
            Calm = 9
        }
    }
}