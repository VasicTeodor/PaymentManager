﻿using Bank.Service.Dto;
using Bank.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Services
{
    public interface IPaymentService
    {
        PaymentRequestResponseDto ValidatePayment(PaymentRequest request);
        bool SubmitPayment(CardDto cardDto);
    }
}
