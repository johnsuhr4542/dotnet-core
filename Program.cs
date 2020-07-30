using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace application {
    public class Program {
        public static void Main(string[] args) {
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((host, config) => {
                    var env = host.HostingEnvironment.EnvironmentName;
                    config.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"messages.{env}.json", optional: true, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                }).Build().Run();
        }
    }
}
