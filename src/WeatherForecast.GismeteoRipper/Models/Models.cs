using System;

namespace WeatherForecast.GismeteoRipper.Models
{
    public class City
    {
        public City(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; }
        public string Id { get; }
    }

    public class Temperature
    {
        public Temperature(double value, double feelsLike)
        {
            Value = value;
            FeelsLike = feelsLike;
        }

        public double Value { get; }
        public double FeelsLike { get; }
    }

    public enum WeatherType : short
    {
        Undefined = -1,
        Sunny = 1,
        Cloudy = 2,
        Rainy = 3,
        Snowy = 4,
        Clear = 5
    }

    public class Wind
    {
        public Wind(double value, WindDirection direction)
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
            NorthWest = 5,
            NorthEast = 6,
            SouthWest = 7,
            SouthEast = 8,
            Calm = 9
        }
    }

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