using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace consoleclient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            HttpClient client = new HttpClient();
            var tokenResponse = (client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                //Address = disco.TokenEndpoint,
                Address = "http://localhost:5000/connect/token",
                ClientId = "consoleclient",
                ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0",

                UserName = "houssamfertaq@gmail.com",
                Password = "H0u$$@m2018",
                //Scope = "api1"
            })).Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
            var content = (client.GetStringAsync("http://localhost:5001/api/product/secure")).Result;
            Console.WriteLine(content);

            Console.ReadLine();
        }
    }
}
