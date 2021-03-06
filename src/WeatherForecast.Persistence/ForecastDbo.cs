using System;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherForecast.Persistence
{
    [BsonIgnoreExtraElements]
    public class ForecastDbo
    {
        public ForecastDbo(TemperatureDbo temperature, WindDbo wind, int pressure, int humidity,
            int geomagneticActivity, double waterTemperature, string weatherType, string city, DateTime date)
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

        public TemperatureDbo Temperature { get; }
        public WindDbo Wind { get; }
        public int Pressure { get; }
        public int Humidity { get; }
        public int GeomagneticActivity { get; }
        public double WaterTemperature { get; }
        public string WeatherType { get; }
        public string City { get; }
        public DateTime Date { get; }
    }
}