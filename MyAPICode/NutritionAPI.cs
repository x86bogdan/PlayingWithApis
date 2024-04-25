using RestSharp;
using RestSharp.Authenticators;

namespace MyAPICode
{
    public class NutritionAPI
    {
        private string _apiURL = "https://api.edamam.com/api/nutrition-data";
        public NutritionAPI()
        {

        }

        public string? GetNutritionData()
        {
            var apiKey = Environment.GetEnvironmentVariable("ApiKey");
            var secretKey = Environment.GetEnvironmentVariable("SecretKey");
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
