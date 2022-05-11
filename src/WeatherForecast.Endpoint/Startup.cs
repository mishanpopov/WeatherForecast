using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using WeatherForecast.Endpoint.GrpcServices;
using WeatherForecast.Persistence;
using WeatherForecast.Persistence.Abstraction;

namespace WeatherForecast.Endpoint
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(sp => new ForecastDbSettings
            {
                ConnectionString = _configuration["ForecastDbSettings:ConnectionString"],
                DatabaseName = _configuration["ForecastDbSettings:DatabaseName"]
            });

            ConventionRegistry.Register(
                "camelCase",
                new ConventionPack {new CamelCaseElementNameConvention()},
                t => true);

            services.AddTransient<IRepository, Repository>();

            services.AddMvc()
                .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy(),
                            
                        };
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    }
                );
            services.AddGrpc();
            services.AddSwaggerGen(c=>
            {
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "WeatherForecast.Endpoint.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<ForecastGrpcService>();
            });
        }
    }
}