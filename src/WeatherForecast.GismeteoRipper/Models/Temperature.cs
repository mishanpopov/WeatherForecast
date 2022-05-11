namespace WeatherForecast.GismeteoRipper.Models
{
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
}