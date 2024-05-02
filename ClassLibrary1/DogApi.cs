using System.Text;
using System.Threading.RateLimiting;

namespace ClassLibrary1
{
    // presupunem ca suntem limitati la 1 request per secunda
    public class DogApiLimited : DogApi
    {
        public DogApiLimited() : base()
        {
            var options = new TokenBucketRateLimiterOptions
            {
                TokenLimit = 1,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 1,
                ReplenishmentPeriod = TimeSpan.FromMilliseconds(1000),
                TokensPerPeriod = 1,
                AutoReplenishment = true
            };

            // Create an HTTP client with the client-side rate limited handler.
            _client = new HttpClient(
                handler: new ClientSideRateLimitedHandler(
                    rateLimiter: new TokenBucketRateLimiter(options)));

            _client.BaseAddress = new Uri("https://dog.ceo/api/");
        }
    }

    public class DogApi
    {
        protected HttpClient _client;
        
        public DogApi()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://dog.ceo/api/");
        }

        public DogApiResponse GetRandomDog(string breed)
        { 
            var response = _client.GetAsync($"breed/{breed}/images/random").Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var message = Newtonsoft.Json.JsonConvert.DeserializeObject<DogApiResponse>(result);

            return message ?? new DogApiResponse();
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