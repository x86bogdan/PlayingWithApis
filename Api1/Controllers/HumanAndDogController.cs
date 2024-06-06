using Api1.DatabaseModels;
using ClassLibrary1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace Api1.Controllers
{
    [ApiController]
    [Route("human")]
    public class HumanAndDogController : Controller
    {
        public IConfiguration _config;
        public HumanAndDogController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [Route("api")]
        [HttpGet()]
        [Produces(typeof(IEnumerable<StudentAndDog>))]
        public IActionResult GetStudentDogsFromApi()
        {
            var api = new HumanAndDog();
            var result = api.GetStudentAndDog(_config);
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
            var result = api.GetStudentAndDog(_config);
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

        [Route("database/[action]")]
        [HttpGet]
        public IActionResult GetStudentDogsByBreed(string breed)
        {
            using (var dbContext = new ApplicationContext())
            {
                var query = @"SELECT Id, FirstName, LastName, DogName, DogBreed, DogImage
FROM StudentDogs
WHERE DogBreed = @breed";
      
                var breedParam = new SqliteParameter("@breed", breed);
                var dogs = dbContext.StudentDogs.FromSqlRaw(query, breedParam);

                // equivalent with: 
                //var dogs = dbContext.StudentDogs.Where(d => d.DogBreed == breed);

                return Ok(dogs.ToList());
            }
        }

        [Route("database/[action]")]
        [HttpGet]
        public IActionResult GetStudentDogsCountByBreed(string breed)
        {
            using (var dbContext = new ApplicationContext())
            {
                var query = @"SELECT @DogCount = Count(Id)
FROM StudentDogs
WHERE DogBreed = @breed
GROUP BY DogBreed";

                // return a result from a query, will work in sql but doesn't work in sqlite
                var breedParam = new SqliteParameter("@breed", breed);
                var dogCountParam = new SqliteParameter("@DogCount", System.Data.SqlDbType.BigInt);
                dogCountParam.Direction = System.Data.ParameterDirection.Output;

                var dogs = dbContext.Database.ExecuteSqlRaw(query, dogCountParam, breedParam);

                return Ok(dogCountParam.Value);
            }
        }

        [Route("database/[action]")]
        [HttpGet]
        public IActionResult GetStudentDogsStatistics()
        {
            using (var dbContext = new ApplicationContext())
            {
                dbContext.Database.BeginTransaction();

                var query = @"SELECT DogBreed, Count(Id) AS No
FROM StudentDogs
GROUP BY DogBreed";
                var result = dbContext.Database.ExecuteSqlRaw(query);

                dbContext.Database.CommitTransaction();

                return Ok(result);
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
