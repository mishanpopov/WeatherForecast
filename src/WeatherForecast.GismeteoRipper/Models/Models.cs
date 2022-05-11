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
}