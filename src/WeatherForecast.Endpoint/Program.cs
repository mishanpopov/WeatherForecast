using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace WeatherForecast.Endpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    // {
                        webBuilder.ConfigureKestrel(options =>
                        {
                            options.ListenAnyIP(5002, o => o.Protocols = HttpProtocols.Http2);
                            options.ListenAnyIP(5001, o => o.Protocols = HttpProtocols.Http1);
                        });
                    // }
                    
                    webBuilder.UseStartup<Startup>();
                });
    }
}