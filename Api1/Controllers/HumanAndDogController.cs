using ClassLibrary1;
using Microsoft.AspNetCore.Mvc;

namespace Api1.Controllers
{
    [ApiController]
    [Route("human")]
    public class HumanAndDogController : Controller
    {
        [HttpGet]
        [Produces(typeof(IEnumerable<StudentAndDog>))]
        public IActionResult Index()
        {
            var api = new HumanAndDog();
            var result = api.GetStudentAndDog();
            return Ok(result);
        }
    }
}
