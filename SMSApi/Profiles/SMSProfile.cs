using AutoMapper;
using SMSApi.Dtos;
using SMSApi.Models;

namespace SMSApi.Profiles
{
    public class SMSProfile : Profile
    {
        public SMSProfile() {
            CreateMap<BasicSMS, SMSRequest>().ReverseMap();
        }
    }
}
