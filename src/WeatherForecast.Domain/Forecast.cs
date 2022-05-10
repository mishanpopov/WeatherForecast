using System;

namespace WeatherForecast.Domain
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

        // todo: BsonId не должно быть тут 
        // [BsonId(IdGenerator = typeof(BsonObjectIdGenerator))]
        // [BsonRepresentation(BsonType.ObjectId)] 
        public Temperature Temperature { get; }
        public Wind Wind { get; }
        public int Pressure { get; }
        public int Humidity { get; }
        public int GeomagneticActivity { get; }
        public double WaterTemperature { get; }
        public string WeatherType { get; }
        public string City { get; }
        public DateTime Date { get; }
    }
}