using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ForecastWeather;
using FuuuWeatherForecast.GismeteoRipper.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherForecast.GismeteoRipper.Models;

namespace WeatherForecast.GismeteoRipper
{
    public class ParseJob : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ParseJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("Go for a forecast");
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var gismeteoClient = scope.ServiceProvider.GetRequiredService<IGismeteoHttpClient>();
                        var weatherForecastClient =
                            scope.ServiceProvider.GetRequiredService<ForecastApi.ForecastApiClient>();
                        var parser = scope.ServiceProvider.GetRequiredService<IParser>();


                        var citiesHtml = await gismeteoClient.GetPopularCitiesHtml();
                        var cities = parser.GetPopularCities(citiesHtml);

                        var forecastList = new List<Forecast>();
                        foreach (var city in cities)
                        {
                            var forecastHtml = await gismeteoClient.GetDetailedCityForecastHtml(city.Id);
                            var forecast = parser.GetForecast(forecastHtml);
                            forecast.City = city.Name;
                            forecastList.Add(forecast);
                        }

                        var saveRequest = new SaveWeatherForecastsRequest();
                        var weatherForecast = forecastList.Select(f => f.ToProto());
                        saveRequest.Forecasts.AddRange(weatherForecast);
                        await weatherForecastClient.SaveWeatherForecastsAsync(saveRequest);
                    }

                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
                }
            }
        }
    }
}