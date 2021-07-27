using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace Creator.FunctionalTests.Base
{
    public class CreatorScenarioBase
    {
        private const string ApiUrlBase = "api/create";

        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(CreatorScenarioBase))
               .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                }).UseStartup<CreatorTestStartup>();

            return new TestServer(hostBuilder);
        }

        public static class Get
        {
            public static string working = $"{ApiUrlBase}/";
        }

        public static class Post
        {
            public static string SMSSendQueue = $"{ApiUrlBase}/sendsmsqueue";
        }
    }
}
