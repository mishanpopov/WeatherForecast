namespace WeatherForecast.Endpoint.Models
{
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