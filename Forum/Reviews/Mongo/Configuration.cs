using System.IO;
using Microsoft.Extensions.Configuration;

namespace Reviews.Mongo
{
    public class Configuration
    {
        public static IConfiguration Load()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..");
            var build = new ConfigurationBuilder().SetBasePath(path).AddJsonFile("mongoPath.json")
                .AddEnvironmentVariables();
            return build.Build();
        }
    }
}