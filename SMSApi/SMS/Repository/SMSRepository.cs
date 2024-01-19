using SMSApi.Data;
using SMSApi.Models;

namespace SMSApi.SMS.Repository
{
    public class SMSRepository : ISMSRepository
    {
        private SMSDbContext _context;

        public SMSRepository(SMSDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BasicSMS> GetSMSes()
        {
            return _context.BasicSMSes.ToList();
        }

        public BasicSMS GetSMSByID(int id)
        {
            return _context.BasicSMSes.Find(id);
        }

        public void InsertSMS(BasicSMS sms)
        {
            _context.BasicSMSes.Add(sms);
        }

        public void DeleteSMS(int smsID)
        {
            BasicSMS sms = _context.BasicSMSes.Find(smsID);
            _context.BasicSMSes.Remove(sms);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
