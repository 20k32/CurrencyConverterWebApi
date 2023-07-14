namespace Models
{
    public class ApiSettings
    {
        public string ApiUrl { get; set; } = null!;
        public string ConvertRelativeUrl { get; set; } = null!;
        public string LastestRelativeUrl { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public string BaseApiUrl => $"{ApiUrl}/{LastestRelativeUrl}?{UserId}";
        public string ConvertApiUrl => $"{ApiUrl}/{ConvertRelativeUrl}";

        public ApiSettings()
        { }
    }
}
