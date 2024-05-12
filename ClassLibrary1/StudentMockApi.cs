using Newtonsoft.Json;

namespace ClassLibrary1
{
    public class StudentMockApi
    {

        public async Task<IEnumerable<Student>> GetStudentData()
        { 
            var client = new HttpClient();
            var respone = await client.GetAsync("https://my.api.mockaroo.com/students?key=c0a6d370");
            var stringResult = await respone.Content.ReadAsStringAsync();
            // deserializam in tipul de date corect, iar daca deserializarea nu reuseste (intoarce null) returnam o lista goala
            var students = JsonConvert.DeserializeObject<IEnumerable<Student>>(stringResult) ?? new List<Student>();
            return students;
        }
    }
}
