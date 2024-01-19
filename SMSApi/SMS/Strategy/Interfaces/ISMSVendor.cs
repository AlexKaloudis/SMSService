using SMSApi.Models;

namespace SMSApi.SMS.Strategy.Interfaces
{
    public interface ISMSVendor
    {
        void Send(BasicSMS sms);
    }
}
