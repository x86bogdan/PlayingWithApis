using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;

namespace MyAPICode
{
    public class NutritionAPI
    {
        private readonly string _apiURL = "https://api.edamam.com/api/nutrition-data";
        private readonly IConfiguration _config;
        public NutritionAPI(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string? GetNutritionData()
        {
            var apiKey = _config["ApiKey"];
            var secretKey = _config["SecretKey"];
            var options = new RestClientOptions(_apiURL)
            {
                Authenticator = new HttpBasicAuthenticator(apiKey, secretKey)
            };
            var client = new RestClient(options);
            var request = new RestRequest("?ingr=milk");
            
            var response = client.GetAsync(request).Result;
            return response.Content;
        }
    }
}
