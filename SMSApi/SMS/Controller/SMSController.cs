using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMSApi.Dtos;
using SMSApi.Models;
using SMSApi.SMS.Repository;
using SMSApi.SMS.Strategy;
using SMSApi.SMS.Strategy.Interfaces;

namespace SMSApi.SMS.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase
    {
        private ISMSVendorContext _vendorContext;
        private ISMSRepository _smsRepository;
        private readonly IMapper _mapper;

        public SMSController(ISMSVendorContext vendorContext, ISMSRepository smsRepository, IMapper mapper)
        {
            _vendorContext = vendorContext;
            _smsRepository = smsRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SMSRequest sms)
        {
            Dictionary<string, Func<ISMSVendor>> vendors = new Dictionary<string, Func<ISMSVendor>>()
            {
                { "+30", () => new SMSVendorGR(_smsRepository) },
                { "+357", () => new SMSVendorCY(_smsRepository) },
                { "default", () => new SMSVendorRest(_smsRepository) }
            };

            Func<ISMSVendor> vendorFactory;
            vendors.TryGetValue(sms.CountryCode, out vendorFactory);

            ISMSVendor vendor = vendorFactory.Invoke();
            _vendorContext.SetVendor(vendor);
            _vendorContext.SendMessage(_mapper.Map<BasicSMS>(sms));

            return CreatedAtAction(nameof(SendMessage), new { Message = sms.Message }, sms);

        }

    }
}
