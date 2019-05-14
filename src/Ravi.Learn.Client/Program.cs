using IdentityModel.Client;
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

        private static async  void Main(string[] args)
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

            //Call API
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"{ApiUrl}/"


        }
    }
}
