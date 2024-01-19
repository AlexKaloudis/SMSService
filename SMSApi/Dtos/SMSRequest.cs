namespace SMSApi.Dtos
{
    public class SMSRequest
    {
        public string CountryCode { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
