using AutoMapper;
using PayPal.Service.Data.Entities;
using PayPal.Service.Dtos;

namespace PayPal.Service.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PaymentRequestDto, PaymentRequest>()
                .ForPath(dest => dest.OrderId,
                    opt => opt.MapFrom(src => src.OrderId))
                .ForPath(dest => dest.Amount,
                    opt => opt.MapFrom(src => src.Amount))
                .ForPath(dest => dest.ErrorUrl,
                    opt => opt.MapFrom(src => src.ErrorUrl))
                .ForPath(dest => dest.Currency,
                    opt => opt.MapFrom(src => src.Currency))
                .ForPath(dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.PaymentIntent,
                    opt => opt.MapFrom(src => src.PaymentIntent))
                .ForPath(dest => dest.PaymentMethod,
                    opt => opt.MapFrom(src => src.PaymentMethod))
                .ForPath(dest => dest.SuccessUrl,
                    opt => opt.MapFrom(src => src.SuccessUrl))
                .ForPath(dest => dest.Id,
                    opt => opt.Ignore())
                .ForPath(dest => dest.PaymentId,
                    opt => opt.Ignore())
                .ForPath(dest => dest.TableVersion,
                    opt => opt.Ignore());
        }
    }
}