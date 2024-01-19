using SMSApi.Models;
using SMSApi.SMS.Repository;
using SMSApi.SMS.Strategy.Interfaces;
using System.Globalization;

namespace SMSApi.SMS.Strategy
{
    public class SMSVendorGR : ISMSVendor
    {
        ISMSRepository _repository;
        public SMSVendorGR(ISMSRepository repository)
        {
            _repository = repository;
        }

        public void Send(BasicSMS sms)
        {
            // Check if the message contains only Greek characters
            if (!sms.Message.All(c =>
               (c >= 0x0370 && c <= 0x03FF) ||
               char.GetUnicodeCategory(c) == UnicodeCategory.SpaceSeparator))
            {
                throw new ArgumentException("Message contains non-Greek characters.");
            }
            _repository.InsertSMS(sms);
            _repository.Save();
        }
    }
}
