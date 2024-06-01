using Microsoft.Extensions.Configuration;

namespace ClassLibrary1
{
    public class HumanAndDog
    {
        public IList<StudentAndDog> GetStudentAndDog(IConfiguration configuration)
        {
            var studentApi = new StudentMockApi(configuration);
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


