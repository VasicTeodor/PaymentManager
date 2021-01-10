using AutoMapper;
using BitCoin.Service.Dtos;
using BitCoin.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitCoin.Service.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<OrderDto, Order>()
                .ForPath(dest => dest.OrderId,
                    opt => opt.MapFrom(src => src.OrderId))
                .ForPath(dest => dest.PriceAmount,
                    opt => opt.MapFrom(src => src.PriceAmount))
                .ForPath(dest => dest.PriceCurrency,
                    opt => opt.MapFrom(src => src.PriceCurrency))
                .ForPath(dest => dest.ReceiveCurrency,
                    opt => opt.MapFrom(src => src.ReceiveCurrency))
                .ForPath(dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForPath(dest => dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.CallbackUrl,
                    opt => opt.MapFrom(src => src.CallbackUrl))
                .ForPath(dest => dest.CancelUrl,
                    opt => opt.MapFrom(src => src.CancelUrl))
                .ForPath(dest => dest.SuccessUrl,
                    opt => opt.MapFrom(src => src.SuccessUrl))
                .ForPath(dest => dest.Token,
                    opt => opt.MapFrom(src => src.Token))
                .ForPath(dest => dest.Id,
                    opt => opt.Ignore())
                .ForPath(dest => dest.TableVersion,
                    opt => opt.Ignore());

            CreateMap<OrderResultDto, OrderResult>()
                .ForPath(dest => dest.Status,
                    opt => opt.MapFrom(src => src.status))
                .ForPath(dest => dest.PriceCurrency,
                    opt => opt.MapFrom(src => src.price_currency))
                .ForPath(dest => dest.PriceAmount,
                    opt => opt.MapFrom(src => src.price_amount))
                .ForPath(dest => dest.ReceiveCurrency,
                    opt => opt.MapFrom(src => src.receive_currency))
                .ForPath(dest => dest.ReceiveAmount,
                    opt => opt.MapFrom(src => src.receive_amount))
                .ForPath(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.created_at))
                .ForPath(dest => dest.OrderId,
                    opt => opt.MapFrom(src => src.order_id))
                .ForPath(dest => dest.PaymentUrl,
                    opt => opt.MapFrom(src => src.payment_url))
                .ForPath(dest => dest.Token,
                    opt => opt.MapFrom(src => src.token))
                .ForPath(dest => dest.Id,
                    opt => opt.Ignore())
                .ForPath(dest => dest.TableVersion,
                    opt => opt.Ignore());
        }
    }
}
