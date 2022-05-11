using System.Linq;
using System.Threading.Tasks;
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

        public override async Task<Empty> SaveWeatherForecasts(SaveWeatherForecastsRequest request,
            ServerCallContext context)
        {
            await _repository.Create(
                request.Forecasts.Select(f => f.ToDomain()),
                context.CancellationToken);
            
            return new Empty();
        }
    }
}