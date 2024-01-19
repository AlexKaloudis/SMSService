using SMSApi.Models;
using SMSApi.SMS.Repository;
using SMSApi.SMS.Strategy.Interfaces;
using System.Globalization;

namespace SMSApi.SMS.Strategy
{
    public class SMSVendorCY : ISMSVendor
    {
        ISMSRepository _repository;
        private const int MaxLength = 160;

        public SMSVendorCY(ISMSRepository repository)
        {
            _repository = repository;
        }
       
        public void Send(BasicSMS sms)
        {
            string[] messages = Split(sms.Message, MaxLength);

            foreach (var msg in messages)
            {
                _repository.InsertSMS(new BasicSMS { 
                    Message = msg,
                    PhoneNumber = sms.PhoneNumber});
                _repository.Save();
            }
        }


        public string[] Split(string value, int desiredLength, bool strict = false)
        {
            EnsureValid(value, desiredLength, strict);
            var stringInfo = new StringInfo(value);

            int currentLength = stringInfo.LengthInTextElements;
            if (currentLength == 0) { return new string[0]; }

            int numberOfItems = currentLength / desiredLength;

            int remaining = (currentLength > numberOfItems * desiredLength) ? 1 : 0;

            var chunks = new string[numberOfItems + remaining];

            for (var i = 0; i < numberOfItems; i++)
            {
                chunks[i] = stringInfo.SubstringByTextElements(i * desiredLength, desiredLength);
            }

            if (remaining != 0)
            {
                chunks[numberOfItems] = stringInfo.SubstringByTextElements(numberOfItems * desiredLength);
            }

            return chunks;
        }

        private void EnsureValid(string value, int desiredLength, bool strict)
        {
            
            if (value == null) { throw new ArgumentNullException(nameof(value)); }

            if (value.Length == 0 && desiredLength != 0)
            {
                throw new ArgumentException($"The passed {nameof(value)} may not be empty if the {nameof(desiredLength)} <> 0");
            }

            var info = new StringInfo(value);
            int valueLength = info.LengthInTextElements;

            if (valueLength != 0 && desiredLength < 1) { throw new ArgumentException($"The value of {nameof(desiredLength)} needs to be > 0"); }

            if (strict && (valueLength % desiredLength != 0))
            {
                throw new ArgumentException($"The passed {nameof(value)}'s length can't be split by the {nameof(desiredLength)}");
            }
        }
    }
}
