using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.GismeteoRipper.Controllers
{
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly Parser _parser;

        public TestController(Parser parser)
        {
            _parser = parser;
        }
        
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            ;
            // var r = await _parser.GetPopularCities(TODO);
            // var rr =await _parser.GetForecast(TODO);
            
            return Ok();
        }
    }
}