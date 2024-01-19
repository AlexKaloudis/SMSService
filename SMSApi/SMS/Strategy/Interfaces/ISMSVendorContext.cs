using SMSApi.Models;

namespace SMSApi.SMS.Strategy.Interfaces
{
    public interface ISMSVendorContext
    {
        public void SetVendor(ISMSVendor vendor);

        public void SendMessage(BasicSMS sms);
    }
}
