using FluentValidation;
using System.Text.RegularExpressions;

namespace BaseActions.Queries.GetSpecificCurrency
{
    public class GetSpecificCurrencyQueryValidator : AbstractValidator<GetSpecificCurrencyQuery>
    {
        public GetSpecificCurrencyQueryValidator()
        {
            RuleFor(getSpecificCurrency => getSpecificCurrency.CurrencyName)
                .NotEmpty()
                .NotNull()
                .Length(3)
                .Must(str => Regex.Match(str, "[A-Z][A-Z][A-Z]").Success);
        }
    }
}
