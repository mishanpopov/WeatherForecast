using System;

namespace WeatherForecast.GismeteoRipper.Models
{
    public class Forecast
    {
        public Forecast(
            Temperature temperature,
            Wind wind,
            int pressure,
            int humidity,
            int geomagneticActivity,
            double waterTemperature,
            string weatherType,
            DateTime date)
        {
            Temperature = temperature;
            Wind = wind;
            Pressure = pressure;
            Humidity = humidity;
            GeomagneticActivity = geomagneticActivity;
            WaterTemperature = waterTemperature;
            WeatherType = weatherType;
            Date = date;
        }

        public Temperature Temperature { get; }
        public Wind Wind { get; }
        public int Pressure { get; }
        public int Humidity { get; }
        public int GeomagneticActivity { get; }
        public double WaterTemperature { get; }
        public string WeatherType { get; }
        public DateTime Date { get; }
        public string City { get; set; }
    }
}
