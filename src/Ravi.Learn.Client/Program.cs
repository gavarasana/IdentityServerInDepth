using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ravi.Learn.Client
{
    class Program
    {
        private static string Authority = "http://localhost:6000";
        private static string ClientId = "Magazine";
        private static string ClientSecret = "greenTrees";
        private static string ApiScope = "MagazinesApi";
        private static string ApiUrl = "https://localhost:5001";

        private static async  Task Main(string[] args)
        {

            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(Authority);
            if (disco.IsError)
            {
                Console.WriteLine($"Error: {disco.Error}");
                await Task.CompletedTask;
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
                await Task.CompletedTask; ;
            }

            Console.WriteLine($"Token: {tokenResponse.Json}");

            //Call API
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"{ApiUrl}/api/Identity");
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
