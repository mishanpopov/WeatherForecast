using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Endpoint.Mapping;
using WeatherForecast.Endpoint.Models;
using WeatherForecast.Persistence.Abstraction;

namespace WeatherForecast.Endpoint.Controllers
{
    [Route("forecast")]
    public class ForecastController : ControllerBase
    {
        private readonly IRepository _repository;

        public ForecastController(IRepository repository) => _repository = repository;

        [HttpGet("popular-cities")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PopularCitiesResponse))]
        public async Task<ActionResult<PopularCitiesResponse>> PopularCities(CancellationToken cancellationToken)
        {
            var popularCityCollection = await _repository.GetPopularCityCollection(cancellationToken);
            return new PopularCitiesResponse {PopularCities = popularCityCollection};
        }


        /// <summary>
        /// Returns a forecast for specified city and date
        /// </summary>
        /// <param name="city" example="Москва"></param>
        /// <param name="date" example="2022-05-10">The date of the forecast, YYYY-MM-DD</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("forecast/{city}/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ForecastDto))]
        public async Task<ActionResult<ForecastDto>> GetForecast(
            [FromRoute] string city,
            [FromRoute] DateTime date,
            CancellationToken cancellationToken)
        {
            var forecast = await _repository.GetForecast(city.Trim(), date, cancellationToken);
            if (forecast == null)
            {
                return NotFound();
            }
            return forecast.ToDto();
        }

        public class PopularCitiesResponse
        {
            public IEnumerable<string> PopularCities { get; set; }
        }
    }
}