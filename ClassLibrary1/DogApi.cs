using System.Text;

namespace ClassLibrary1
{
    public class DogApi
    {
        private HttpClient _client;
        
        public DogApi()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://dog.ceo/api/");
        }

        public string GetRandomDog(string breed)
        { 
            var response = _client.GetAsync($"breed/{breed}/images/random").Result;
            var result = response.Content.ReadAsStringAsync().Result;

            return result;
        }

        public async Task<DogApiResponse> GetRandomDogAsync(string breed)
        {
            var response = await _client.GetAsync($"breed/{breed}/images/random");
            var result = await response.Content.ReadAsStringAsync();
            var message = Newtonsoft.Json.JsonConvert.DeserializeObject<DogApiResponse>(result);

            return message ?? new DogApiResponse();
        }
    }
}