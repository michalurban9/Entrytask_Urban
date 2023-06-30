using Entrytask_Urban.Controllers;
using Entrytask_Urban.Models;
using Microsoft.AspNetCore.Mvc;
namespace Entrytask_Urban.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class WeatherForecastControll : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastControll> _logger;

        public WeatherForecastControll(ILogger<WeatherForecastControll> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
         
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
