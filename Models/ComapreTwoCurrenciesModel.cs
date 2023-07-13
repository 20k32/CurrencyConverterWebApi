namespace Models
{
    public class ComapreTwoCurrenciesModel
    {
        public CurrencyMainModel Left { get; private set; } = null!;
        public CurrencyMainModel Right { get; private set; } = null!;

        public ComapreTwoCurrenciesModel(CurrencyMainModel left, CurrencyMainModel right) =>
            (Left, Right) = (left, right);

        public bool IsLeftCurrencyValueGreaterThanRight => Left.Value > Right.Value;

        public ValueTuple<double, double> GetLeftAndRightCurrenciesValuesSeeingAmount() =>
            (Left.AmountValue, Right.AmountValue);

        public void SwapCurrencies() =>
            (Left, Right) = (Right, Left);
    }
}
