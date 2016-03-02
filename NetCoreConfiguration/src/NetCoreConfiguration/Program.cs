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
            var environment = "development";
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: true);
            var configuration = builder.Build();

            // Is "1.0.0" (from appsettings.json)
            string version = configuration["Version"];

            // Is "test-key" (from appsettings.development.json)
            string apiKey = configuration["ApiKey"];

            // Is "http://test.myapi.com/" (from appsettings.development.json)
            string apiAddress = configuration["ApiAddress"];

            // Is ["Steve", "Lucy", "Harry"] (from appsettings.json)
            var users = configuration.GetSection("Auth:Users").GetChildren().Select(x => x.Value);

            ///// ------------- From appsettings.json ------------ /////   /// From appsettings.development.json ///
            // Is ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday",          "Saturday", "Sunday"]
            var availableDays = configuration.GetSection("ServerAvailable:Days").GetChildren().Select(x => x.Value);

            // Is [12] (from appsettings.development.json)
            var updateAtHours = configuration.GetSection("UpdatesOn:Hours").GetChildren().Select(x => x.Value);

            Console.ReadLine();
        }
    }
}
