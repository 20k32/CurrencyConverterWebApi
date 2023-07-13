using MediatR.NotificationPublishers;

namespace CurrencyConverter.WebApi
{
    public class CurrencyFromQueryDTO
    {
        public string Name { get; set; } = null!;
        public uint Amount { get; set; }
    }

    public class CurrencyFromQueryModel
    {
        public CurrencyFromQueryDTO Left { get; set; } = null!;
        public CurrencyFromQueryDTO Right { get; set; } = null!;
    }
}
