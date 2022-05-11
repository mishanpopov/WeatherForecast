using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using WeatherForecast.Domain;
using WeatherForecast.Persistence.Abstraction;

namespace WeatherForecast.Persistence
{
    public class Repository : IRepository
    {
        private const string CollectionName = "forecast.snapshots";

        private readonly IMongoCollection<ForecastDbo> _forecastCollection;

        public Repository(ForecastDbSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            _forecastCollection = client
                .GetDatabase(dbSettings.DatabaseName)
                .GetCollection<ForecastDbo>(CollectionName);
        }

        public Task Create(IEnumerable<Forecast> forecast, CancellationToken cancellationToken)
        {
            return _forecastCollection.InsertManyAsync(
                forecast.Select(f => f.ToDbo()),
                cancellationToken: cancellationToken);
        }

        public async Task<Forecast> GetForecast(
            string city,
            DateTime date,
            CancellationToken cancellationToken)
        {
            var filter = Builders<ForecastDbo>.Filter.Eq("city", city.ToLower())
                         & Builders<ForecastDbo>.Filter.Gte("date", date.Date)
                         & Builders<ForecastDbo>.Filter.Lt("date", date.Date.AddDays(1));

            var dbo = await _forecastCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);

            return dbo?.ToDomain();
        }

        public async Task<IEnumerable<string>> GetPopularCityCollection(CancellationToken cancellationToken)
        {
            var filter = new BsonDocument();
            return (await _forecastCollection.DistinctAsync<string>(
                "city",
                filter,
                cancellationToken: cancellationToken)).ToList();
        }
    }
}