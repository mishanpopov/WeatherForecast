namespace WeatherForecast.Persistence
{
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
}