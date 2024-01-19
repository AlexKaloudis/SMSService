using SMSApi.Models;
using SMSApi.SMS.Strategy.Interfaces;

namespace SMSApi.SMS.Strategy
{
    public class SMSVendorContext : ISMSVendorContext
    {
        private ISMSVendor _vendor;

        public SMSVendorContext()
        {
        }
        public SMSVendorContext(ISMSVendor vendor)
        {
            _vendor = vendor;
        }

        public void SetVendor(ISMSVendor vendor)
        {
            _vendor = vendor;
        }

        public void SendMessage(BasicSMS sms)
        {
            Console.WriteLine("Context: Sending message using the chosen vendor");
             _vendor.Send(sms);
        }
    }
}
