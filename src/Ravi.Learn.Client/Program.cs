using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ravi.Learn.Client
{
    class Program
    {
        private static string Authority = "http://localhost:50762";
        private static string ClientId = "Magazine";
        private static string ClientSecret = "greenTrees";
        private static string ApiScope = "MagazinesApi BooksApi";

        private static string MagazineUrl = "http://localhost:64861";        
        private static string BookUrl = "http://localhost:56735";


        private static async  Task Main(string[] args)
        {

            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(Authority);
            if (disco.IsError)
            {
                Console.WriteLine($"Error: {disco.Error}");
                return;
            }

            //request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                Scope = ApiScope
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine($"Error: {tokenResponse.Error}");
                return;
            }

            Console.WriteLine($"Token: {tokenResponse.Json}");

            await InvokeMagazines(tokenResponse);

            await InvokeBooks(tokenResponse);

            Console.WriteLine("Press any key...");

            Console.ReadKey();


        }

        private static async Task InvokeBooks(TokenResponse tokenResponse)
        {
            //Call API
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"{BookUrl}/api/Values");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode}-{response.ReasonPhrase}");
                await Task.CompletedTask; ;
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            await Task.CompletedTask;
        }

        private static async Task InvokeMagazines(TokenResponse tokenResponse)
        {
            //Call API
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"{MagazineUrl}/api/Identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode}-{response.ReasonPhrase}");
                await Task.CompletedTask; ;
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));
            await Task.CompletedTask;
        }
    }
}
