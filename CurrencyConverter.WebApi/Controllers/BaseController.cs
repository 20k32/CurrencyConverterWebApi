using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;

namespace CurrencyConverter.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        protected readonly HttpClient Client = null!;
        protected readonly ApiSettings Settings = null!;

        public BaseController(HttpClient client, IOptions<ApiSettings> settings) =>
            (Client, Settings) = (client, settings.Value);
    }
}
