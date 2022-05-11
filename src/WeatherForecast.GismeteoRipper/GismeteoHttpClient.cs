using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherForecast.GismeteoRipper
{
    public class GismeteoHttpClient : IGismeteoHttpClient
    {
        private readonly HttpClient _client;

        public GismeteoHttpClient(HttpClient client)
        {
            _client = client;
        }

        public Task<string> GetPopularCitiesHtml() => _client.GetStringAsync(string.Empty);

        public Task<string> GetDetailedCityForecastHtml(string cityId)
            => _client.GetStringAsync($"{cityId}/now");
    }
}