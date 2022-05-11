using System.Collections.Generic;
using WeatherForecast.GismeteoRipper.Models;

namespace WeatherForecast.GismeteoRipper
{
    public interface IParser
    {
        IEnumerable<City> GetPopularCities(string html);
        WeatherForecast.GismeteoRipper.Models.Forecast GetForecast(string html);
    }
}