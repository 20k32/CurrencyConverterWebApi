using BaseActions.Queries.GetCurrencyList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Bson;

namespace CurrencyConverter.WebApi.Controllers
{

    public class WeatherForecastController
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly HttpClient Client = null!;
        private IMediator Mediator = null!;
        private ApiSettings InnerApiSettigs = null!;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, HttpClient client, IMediator mediator, ApiSettings settings)
        {
            Client = client;
            _logger = logger;
            Mediator = mediator;
            InnerApiSettigs = settings;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet(Name = "GetExchangeRates")]
        public async Task<IActionResult> GetCurrencyRates()
        {
            Client.DefaultRequestHeaders.Add("Accept", "application/json");

            var resultStr = await Client.GetStringAsync("https://openexchangerates.org/api/latest.json?app_id=fa2e095f3eda4c309ed055db13147511");
            
            //var result = Mediator.Send(new GetCurrencyListQuery()).Result;
            return new JsonResult(resultStr/*.CurrencyList*/);
        }

        [HttpGet(Name = "GetConnectionString")]
        public string GetString()
        {
            return InnerApiSettigs.CurrentApiUrl;
        }

    }
}