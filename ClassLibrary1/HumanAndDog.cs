using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class HumanAndDog
    {
        public IList<StudentAndDog> GetStudentAndDog()
        {
            var studentApi = new StudentMockApi();
            var students = studentApi.GetStudentData().Result;
            var response = new List<StudentAndDog>();
            var dogApi = new DogApi();

            if (students == null)
            {
                return response;
            }

            foreach (var student in students)
            {
                var result = new StudentAndDog();
                result.FirstName = student.FirstName;
                result.LastName = student.LastName;
                result.DogBreed = student.Dog;
                var image = dogApi.GetRandomDog(student.Dog).Message;
                result.DogImage = image;

                response.Add(result);
            }

            return response;
        }

    }
}


