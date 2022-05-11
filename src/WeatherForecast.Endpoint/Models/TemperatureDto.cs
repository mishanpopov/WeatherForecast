namespace WeatherForecast.Endpoint.Models
{
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
}