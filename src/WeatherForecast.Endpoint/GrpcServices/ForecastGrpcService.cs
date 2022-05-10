using System;
using System.Linq;
using System.Threading.Tasks;
using ForecastWeather;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using WeatherForecast.Endpoint.Mapping;
using WeatherForecast.Persistence.Abstraction;

namespace WeatherForecast.Endpoint.GrpcServices
{
    public class ForecastGrpcService : ForecastApi.ForecastApiBase
    {
        private readonly IRepository _repository;

        public ForecastGrpcService(IRepository repository) => _repository = repository;

        public override async Task<Empty> SaveWeatherForecasts(SaveWeatherForecastsRequest request, ServerCallContext context)
        {
            try
            {
                var domainModels = request.Forecasts.Select(f => f.ToDomain());
                await _repository.Create(domainModels, context.CancellationToken);
                return new Empty();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}