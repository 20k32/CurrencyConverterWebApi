namespace Models
{
    public class ApiRequestModel
    {
        public string Disclaimer { get; set; } = null!;
        public string License { get; set; } = null!;
        public uint Timestamp { get; set; }
        public string Base { get; set; } = null!;
        public Dictionary<string, double> Rates { get; set; } = null!;

        public ApiRequestModel()
        { }
    }
}
