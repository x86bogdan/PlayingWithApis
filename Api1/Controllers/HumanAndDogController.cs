using Api1.DatabaseModels;
using ClassLibrary1;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
    [ApiController]
    [Route("human")]
    public class HumanAndDogController : Controller
    {
        [Route("api")]
        [HttpGet()]
        [Produces(typeof(IEnumerable<StudentAndDog>))]
        public IActionResult GetStudentDogsFromApi()
        {
            var api = new HumanAndDog();
            var result = api.GetStudentAndDog();
            return Ok(result);
        }

        [Route("database")]
        //[HttpGet(Name = "GetStudentDogs")]
        [HttpGet]
        public IActionResult GetStudentDogs()
        {
            using (var appContext = new ApplicationContext())
            {
                var dogs = appContext.StudentDogs.ToList();

                return Ok(dogs);
            }
        }

        [Route("database")]
        //[HttpPost(Name = "AddStudentDogs")]
        [HttpPost]
        public IActionResult AddStudentDogs()
        { 
            using var appContext = new ApplicationContext();

            appContext.Database.EnsureCreated();

            var api = new HumanAndDog();
            var result = api.GetStudentAndDog();
            var oneResult = result.FirstOrDefault();

            var myDog = new StudentDog
            { 
                FirstName = oneResult.FirstName,
                LastName = oneResult.LastName,
                DogName = "dog",
                DogImage = oneResult.DogImage,
                DogBreed = oneResult.DogBreed
            };

            appContext.StudentDogs.Add(myDog);
            appContext.SaveChanges();

            var dogs = appContext.StudentDogs.ToList();

            return Ok(dogs);
        }

        [Route("database/{id}")]
        [HttpGet]
        public IActionResult GetStudentDogs([FromRoute]int id)
        {
            using (var appContext = new ApplicationContext())
            {
                var dog = appContext.StudentDogs.Where(d => d.Id == id).FirstOrDefault();
                return Ok(dog);
            }
        }

        [Route("database")]
        [HttpGet]
        public IActionResult GetStudentDogsByBreed(string breed)
        {
            using (var appContext = new ApplicationContext())
            {
                //appContext.Database.BeginTransaction();

                //appContext.Database.CommitTransaction();

                //var x = from appContext.StudentDogs
                //        where appContext.StudentDogs.Count() > 0
                //        select sudasd

                var dogs = appContext.StudentDogs.Where(d => d.DogBreed == breed);
                //var dogs = appContext.StudentDogs.Where(d => d.Id > 0);

                return Ok(dogs);
            }
        }

        [Route("database/{id}")]
        [HttpDelete]
        public IActionResult DeleteStudentDogs(int id)
        {
            using (var appContext = new ApplicationContext())
            {
                try
                {
                    var dog = appContext.StudentDogs.Where(d => d.Id == id).FirstOrDefault();
                    appContext.StudentDogs.Remove(dog);
                    appContext.SaveChanges();

                    return Ok(dog);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Eroare: {ex.Message}");
                }
            }
        }

    }
}
