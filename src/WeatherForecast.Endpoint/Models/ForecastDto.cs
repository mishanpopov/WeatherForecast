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
}