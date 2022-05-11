using System.Threading.Tasks;

namespace WeatherForecast.GismeteoRipper
{
    public interface IGismeteoHttpClient
    {
        Task<string> GetPopularCitiesHtml();
        Task<string> GetDetailedCityForecastHtml(string cityId);
    }
}