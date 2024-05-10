using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class StudentMockApi
    {

        public async Task<string> GetStudentData()
        { 
            var client = new HttpClient();
            var respone = await client.GetAsync("https://my.api.mockaroo.com/students?key=c0a6d370");
            return await respone.Content.ReadAsStringAsync();
        }
    }
}
