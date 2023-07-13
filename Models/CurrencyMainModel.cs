namespace Models
{
    public class CurrencyMainModel
    {
        public string Name { get; set; } = null!;
        public double Value { get; set; }
        public uint Amount { get; set; }
        public double AmountValue => Amount / Value;

        public CurrencyMainModel(string name, double value, uint amount) =>
            (Name, Value, Amount) = (name, value, amount);
    }
}
