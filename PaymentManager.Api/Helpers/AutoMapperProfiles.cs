using AutoMapper;
using PaymentManager.Api.Dtos;
using PaymentManager.Api.Data.Entities;
using PaymentManager.Api.Models;
using PaymentRequest = PaymentManager.Api.Models.PaymentRequest;

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

            CreateMap<PaymentRequestDto, PaymentRequest>()
                .ForPath(dest => dest.Amount,
                    opt => opt.MapFrom(src => src.Amount));

            CreateMap<TransactionResultDto, Transaction>()
                .ForPath(dest => dest.AcquirerOrderId,
                    opt => opt.MapFrom(src => src.AcquirerOrderId))
                .ForPath(dest => dest.AcquirerTimeStamp,
                    opt => opt.MapFrom(src => src.AcquirerTimestamp))
                .ForPath(dest => dest.Amount,
                    opt => opt.MapFrom(src => src.Amount))
                .ForPath(dest => dest.MerchantOrderId,
                    opt => opt.MapFrom(src => src.MerchantOrderId))
                .ForPath(dest => dest.PaymentId,
                    opt => opt.MapFrom(src => src.PaymentId))
                .ForPath(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status));

            CreateMap<PaymentRequest, Data.Entities.PaymentRequest>()
                .ForPath(dest => dest.MerchantOrderId,
                    opt => opt.MapFrom(src => src.MerchantOrderId))
                .ForPath(dest => dest.Amount,
                    opt => opt.MapFrom(src => src.Amount))
                .ForPath(dest => dest.MerchantTimestamp,
                    opt => opt.MapFrom(src => src.MerchantTimestamp))
                .ForPath(dest => dest.TableVersion,
                    opt => opt.Ignore())
                .ForPath(dest => dest.MerchantId,
                    opt => opt.Ignore())
                .ForPath(dest => dest.Id,
                    opt => opt.Ignore());

            CreateMap<PaymentService, PaymentServiceDto>()
                .ForPath(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.PaymentManagerUrl,
                    opt => opt.MapFrom<CustomResolverForPaymentManagerUrl>());

            CreateMap<CreatePaymentServiceDto, PaymentService>()
                .ForPath(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.IsPassTrough,
                    opt => opt.MapFrom(src => src.IsPassTrough))
                .ForPath(dest => dest.Url,
                    opt => opt.MapFrom(src => src.Url))
                .ForPath(dest => dest.Id,
                    opt => opt.Ignore())
                .ForPath(dest => dest.TableVersion,
                    opt => opt.Ignore())
                .ForPath(dest => dest.WebStores,
                    opt => opt.Ignore());

            CreateMap<AddMerchantDto, Merchant>()
                .ForPath(dest => dest.MerchantPassword,
                    opt => opt.MapFrom(src => src.MerchantPassword))
                .ForPath(dest => dest.MerchantUniqueId,
                    opt => opt.MapFrom(src => src.MerchantUniqueId))
                .ForPath(dest => dest.MerchantUniqueStoreId,
                    opt => opt.MapFrom(src => src.MerchantUniqueStoreId))
                .ForPath(dest => dest.Id,
                    opt => opt.Ignore())
                .ForPath(dest => dest.TableVersion,
                    opt => opt.Ignore())
                .ForPath(dest => dest.PaymentServices,
                    opt => opt.Ignore())
                .ForPath(dest => dest.WebStore,
                    opt => opt.Ignore());
        }
    }

    public class CustomResolverForPaymentManagerUrl : IValueResolver<PaymentService, PaymentServiceDto, string>
    {
        public string Resolve(PaymentService source, PaymentServiceDto destination, string member, ResolutionContext context)
        {
            return source.IsPassTrough ? "https://localhost:5021/api/payment/paybypaymentcard" : null;
        }
    }
}