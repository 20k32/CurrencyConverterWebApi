namespace Models
{
    public class CurrencyListItemModel
    {
        public string Name { get; set; } = null!;
        public double Value { get; set; }

        public CurrencyListItemModel()
        { }

        public CurrencyListItemModel(string name, double value) =>
            (Name, Value) = (name, value);
    }
}