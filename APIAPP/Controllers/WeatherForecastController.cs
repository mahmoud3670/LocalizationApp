using APIAPP.Filters;
using LocalizationLibrary;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text.RegularExpressions;

namespace APIAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(LocalizationFilter))]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IStringLocalizer<CommonResources> _localizer;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IStringLocalizer<CommonResources> localizer,
            IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _localizer = localizer;
            _httpContext=httpContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get(/*[FromHeader(Name = "language")] string language = "en-eg"*/)
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary =_localizer[Summaries[Random.Shared.Next(Summaries.Length)] ] 
            })
            .ToArray();
        }
    }
}