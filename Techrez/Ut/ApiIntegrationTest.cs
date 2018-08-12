using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Ut
{
    [TestClass]
    public class ApiIntegrationTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var webHostBuilder =
                  new WebHostBuilder()
                        .UseEnvironment("Development") // You can set the environment you want (development, staging, production)
                        .UseStartup<Startup>(); // Startup class of your web app project

            using (var server = new TestServer(webHostBuilder))
            using (var client = server.CreateClient())
            {
                string result = await client.GetStringAsync("/api/values");
                Assert.AreEqual("[\"value1\",\"value2\"]", result);
            }
        }
    }
}
