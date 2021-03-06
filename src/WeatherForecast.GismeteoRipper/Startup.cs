using System;
using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherForecast.GismeteoRipper
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            services.AddHttpClient<IGismeteoHttpClient, GismeteoHttpClient>(client =>
            {
                var f = _configuration["GismetioEndpoint"];
                client.BaseAddress = new Uri(_configuration["GismetioEndpoint"]);
            });
            services.AddGrpcClient<ForecastApi.ForecastApiClient>(options =>
            {
                var endpoint = _configuration["WeatherForecastService:Endpoint"];
                var port = _configuration["WeatherForecastService:Port"];

                options.Address = new Uri($"http://{endpoint}:{port}");
                options.ChannelOptionsActions.Add(channelOptions
                    => channelOptions.Credentials = ChannelCredentials.Insecure);
            });
            services.AddScoped<IParser, Parser>();
            services.AddHostedService<ParseJob>();
            // services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }
            //
            // app.UseRouting();
            // app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}