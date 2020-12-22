using AutoMapper;
using Bank.Service.Data.Entities;
using Bank.Service.Dto;
using Bank.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Payment, PaymentRequestResponseDto>()
                .ForPath(dest => dest.PaymentId,
                    opt => { opt.MapFrom(src => src.Id); })
                .ForPath(dest => dest.PaymentUrl,
                    opt => { opt.MapFrom(src => src.Url); });
            //CreateMap<PaymentRequest, Payment>()
            //    .ForPath(dest=> dest.Amount, opt=> { opt.MapFrom(src => src.Amount); })
            //    .ForPath(dest=> dest.Status, opt=> { opt.MapFrom(src=> src.)})

        }
    }
}
