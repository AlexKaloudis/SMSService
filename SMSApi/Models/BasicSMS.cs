namespace SMSApi.Models
{
    public class BasicSMS
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
