using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Task t = MainFunction();
            Task.WaitAll(new Task[] { t });

            Console.ReadLine();
        }

        static async Task MainFunction()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            //var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = disco.TokenEndpoint,

            //    ClientId = "client",
            //    ClientSecret = "secret",
            //    Scope = "api1",

            //});

            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",

                UserName = "alice",
                Password = "password",
                Scope = "api1"
            });

            if (response.IsError)
            {
                Console.WriteLine(response.Error);
                return;
            }

            Console.WriteLine(response.Json);

            // call api
            client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            var response2 = await client.GetAsync("http://localhost:5001/identity");
            if (!response2.IsSuccessStatusCode)
            {
                Console.WriteLine(response2.StatusCode);
            }
            else
            {
                var content = await response2.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
