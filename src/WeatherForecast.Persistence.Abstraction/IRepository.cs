using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Domain;

namespace WeatherForecast.Persistence.Abstraction
{
    public interface IRepository
    {
        Task Create(IEnumerable<Forecast> forecast, CancellationToken cancellationToken);
        Task<Forecast> GetForecast(string city, DateTime date, CancellationToken cancellationToken);
        Task<IEnumerable<string>> GetPopularCityCollection(CancellationToken cancellationToken);
    }
}