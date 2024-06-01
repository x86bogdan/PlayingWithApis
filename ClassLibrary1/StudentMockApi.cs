using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ClassLibrary1
{
    public class StudentMockApi
    {
        private readonly IConfiguration _config;
        public StudentMockApi(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<IEnumerable<Student>> GetStudentData()
        { 
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://my.api.mockaroo.com/");
            var respone = await client.GetAsync($"sudents_v2?key={_config["MokarooApiKey"]}");
            var stringResult = await respone.Content.ReadAsStringAsync();
            // deserializam in tipul de date corect, iar daca deserializarea nu reuseste (intoarce null) returnam o lista goala
            var students = JsonConvert.DeserializeObject<IEnumerable<Student>>(stringResult) ?? new List<Student>();
            return students;
        }

        public int TestMethod(bool? input)
        {
            if (input == null)
            {
                return 500;
            }
            if (input == true)
            {
                return 200;
            }
            else
            {
                return 404;
            }
        }
    }
}
