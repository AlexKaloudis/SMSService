using SMSApi.Models;
using SMSApi.SMS.Repository;
using SMSApi.SMS.Strategy.Interfaces;

namespace SMSApi.SMS.Strategy
{
    public class SMSVendorRest : ISMSVendor
    {
        private ISMSRepository _repository;

        public SMSVendorRest(ISMSRepository repository)
        {
            _repository = repository;
        }
        public void Send(BasicSMS sms)
        {
            _repository.InsertSMS(sms);
            _repository.Save();
        }
    }
}
