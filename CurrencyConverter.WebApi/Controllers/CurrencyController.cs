using BaseActions.Commands.SetDataCommand;
using BaseActions.Queries.GetCurrencyLike;
using BaseActions.Queries.GetCurrencyList;
using BaseActions.Queries.GetSpecificCurrency;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Models;
using System.Data.Common;
using System.Xml.Linq;

namespace CurrencyConverter.WebApi.Controllers
{
    public class CurrencyController : BaseController
    {
        private const string CACHE_VALUE_KEY = "Rates";
        private IMediator Mediator;
        private IMemoryCache Cache;

        public CurrencyController(HttpClient client, IOptions<ApiSettings> settings, IMediator mediator, IMemoryCache cache)
            : base(client, settings) =>
        (Mediator, Cache) = (mediator, cache);

        [NonAction]
        private async Task UpdateDatabseAndMemoryCache()
        {
            Client.DefaultRequestHeaders.Add("Accept", "application/json");
            var apiModel = await Client.GetFromJsonAsync<ApiRequestModel>(Settings.CurrentApiUrl);
            if (apiModel != null)
            {
                await Mediator.Send(new SetDataCommandQuery(apiModel));
                Cache.Set(CACHE_VALUE_KEY, apiModel, TimeSpan.FromHours(1));
            }
        }

        [HttpGet(Name = "GetExchangeRates")]
        public async Task<IActionResult> GetCurrencyRates()
        {
            CurrencyListModel result = new();

            if (!Cache.TryGetValue<ApiRequestModel>(CACHE_VALUE_KEY, out var _))
            {
                await UpdateDatabseAndMemoryCache();
            }

            result = await Mediator.Send(new GetCurrencyListQuery());

            return new JsonResult(result.CurrencyList);
        }

        [HttpGet(Name = "GetConnectionString")]
        public IActionResult GetString()
        {
            return Content(Settings.CurrentApiUrl);
        }

        [HttpGet(Name = "GetCurrency/{currency}")]
        public async Task<IActionResult> GetSpecificCurrency(string currency)
        {
            CurrencyListItemModel? result = new();

            if (!Cache.TryGetValue<ApiRequestModel>(CACHE_VALUE_KEY, out var _))
            {
                await UpdateDatabseAndMemoryCache();
            }

            result = await Mediator.Send(new GetSpecificCurrencyQuery(currency));

            if (result == null)
            {
                return new JsonResult(new { Name = "Currency not Found" });
            }

            return new JsonResult(result);
        }

        [HttpGet(Name = "GetCurrenciesLike/{currency}")]
        public async Task<IActionResult> GetCurrenciesLike(string currency)
        {
            CurrencyListModel? result = new();

            if (!Cache.TryGetValue<ApiRequestModel>(CACHE_VALUE_KEY, out var _))
            {
                await UpdateDatabseAndMemoryCache();
            }

            result = await Mediator.Send(new GetCurrencyLikeQuery(currency));

            if (result.CurrencyList.Count == 0)
            {
                return new JsonResult(new { Name = "No matches with this result" });
            }

            return new JsonResult(result.CurrencyList);
        }

        [HttpGet(Name = "CompareCurrencies")]
        public async Task<IResult> CompareCurrencies([FromQuery] CurrencyFromQueryModel? model)
        {
            if(model == null || model.Left == null || model.Right == null)
            {
                Results.Json(new { Name = "Incorrect condition" });
            }

            if (!Cache.TryGetValue<ApiRequestModel>(CACHE_VALUE_KEY, out var _))
            {
                await UpdateDatabseAndMemoryCache();
            }

            var leftCurrency = await Mediator.Send(new GetSpecificCurrencyQuery(model!.Left!.Name));
            var rightCurrency = await Mediator.Send(new GetSpecificCurrencyQuery(model!.Right!.Name));

            if (leftCurrency == null
               || rightCurrency == null)
            {
                Results.Json(new { Name = "Incorrect condition" });
            }
            var leftCurrencyMainModel = new CurrencyMainModel(leftCurrency!.Name, leftCurrency.Value, model.Left.Amount);
            var rightCurrencyMainModel = new CurrencyMainModel(rightCurrency!.Name, rightCurrency.Value, model.Right.Amount);

            return Results.Json(new ComapreTwoCurrenciesModel(leftCurrencyMainModel, rightCurrencyMainModel));
        }
    }
}
