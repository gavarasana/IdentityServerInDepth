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

            var tokenViaClientCredentialFlow = await GetTokenUsingClientCredentialFlow();

            await InvokeMagazines(tokenViaClientCredentialFlow);

            //await InvokeBooks(tokenViaClientCredentialFlow);

            var tokenViaROFlow = await GetTokenUsingROFlow();

            await InvokeMagazines(tokenViaROFlow);


            Console.WriteLine("Press any key...");

            Console.ReadKey();


        }

        private static async Task<TokenResponse> GetTokenUsingROFlow()
        {
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(Authority);
            if (disco.IsError)
            {
                Console.WriteLine($"Error: {disco.Error}");
                return null;
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",
                UserName = "keving",
                Password = "passw0rd",
                Scope = "MagazinesApi"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine($"Error: {tokenResponse.Error}");
                return null;
            }

            Console.WriteLine($"Token: {tokenResponse.Json}");
            return tokenResponse;

        }

        private static async Task<TokenResponse> GetTokenUsingClientCredentialFlow()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(Authority);
            if (disco.IsError)
            {
                Console.WriteLine($"Error: {disco.Error}");
                return null;
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
                return null;
            }

            Console.WriteLine($"Token: {tokenResponse.Json}");

            return tokenResponse;
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

            Console.WriteLine("===================================================");
            Console.WriteLine(JArray.Parse(content));
            Console.WriteLine("===================================================");

            await Task.CompletedTask;
        }
    }
}
