using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace NetCoreConfiguration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // In ASP.Net Apps this can come from StartUp.cs - Constructor - being injected with IHostingEnvironment.EnvironmentName
            var environment = "development";

            // Set up configuration sources.
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: true);
            IConfigurationRoot configuration = builder.Build();

            // Is "1.0.0" (from appsettings.json)
            string version = configuration["Version"];

            // Is "my-dev-key" (from appsettings.development.json)
            string apiKey = configuration["ApiKey"];

            // Is "http://test.myapi.com/" (from appsettings.development.json)
            string apiAddress = configuration["ApiAddress"];

            /////---from appsettings.json----////   ///----from appsettings.development.json----///
            // Is ["Steve", "Lucy",                                    "Alien"]
            IEnumerable<string> users = configuration.GetSection("Auth:Users").GetChildren().Select(x => x.Value);

            /////------------- From appsettings.json------------/////        /// From appsettings.development.json ///
            // Is ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday",             "Saturday", "Sunday"]
            IEnumerable<string> availableDays = configuration.GetSection("ServerAvailable:Days").GetChildren().Select(x => x.Value);

            // Is [12] (from appsettings.development.json)
            IEnumerable<string> updateAtHours = configuration.GetSection("UpdatesOn:Hours").GetChildren().Select(x => x.Value);

            Console.ReadLine();
        }
    }
}
