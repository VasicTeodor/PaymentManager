using AutoMapper;
using PaymentManager.Api.Dtos;
using PaymentManager.Api.Data.Entities;

namespace PaymentManager.Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, User>()
                .ForPath(dest => dest.UserName,
                    opt => { opt.MapFrom(src => src.UserName); })
                .ForPath(dest => dest.Email,
                    opt => { opt.MapFrom(src => src.Email); })
                .ForPath(dest => dest.UserType,
                    opt => { opt.MapFrom(src => src.UserType); });
        }
    }
}