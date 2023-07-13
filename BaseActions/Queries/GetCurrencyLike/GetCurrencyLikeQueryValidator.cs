using BaseActions.Queries.GetSpecificCurrency;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BaseActions.Queries.GetCurrencyLike
{
    public class GetCurrencyLikeQueryValidator : AbstractValidator<GetCurrencyLikeQuery>
    {
        public GetCurrencyLikeQueryValidator()
        {
            RuleFor(x => x.CurrencyName)
                .NotEmpty()
                .NotNull()
                .Length(0, 3);
        }
    }
}
