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
            // Id = ObjectId.GenerateNewId();
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

        // [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        // public string Id { get; set; }
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

    public class TemperatureDbo
    {
        public TemperatureDbo(double value, double feelsLike)
        {
            Value = value;
            FeelsLike = feelsLike;
        }

        public double Value { get; }
        public double FeelsLike { get; }
    }

    public class WindDbo
    {
        public WindDbo(double value, WindDirection direction)
        {
            Value = value;
            Direction = direction;
        }

        public double Value { get; }
        public WindDirection Direction { get; }

        public enum WindDirection : short
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