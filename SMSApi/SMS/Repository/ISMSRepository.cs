using Microsoft.EntityFrameworkCore;
using SMSApi.Models;

namespace SMSApi.SMS.Repository
{
    public interface ISMSRepository
    {
        public IEnumerable<BasicSMS> GetSMSes();
        public BasicSMS GetSMSByID(int id);
        public void InsertSMS(BasicSMS sms);
        public void DeleteSMS(int smsID);
        public void Save();
    }
}