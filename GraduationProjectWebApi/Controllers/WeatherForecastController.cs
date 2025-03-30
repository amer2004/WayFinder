using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectWebApi.Controllers
{
    [ApiController]
    [Route($"{nameof(T)}/[Action]")]
    public class WeatherForecastController<T> : ControllerBase where T : Entity
    {
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
