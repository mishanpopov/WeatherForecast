namespace WeatherForecast.Persistence
{
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