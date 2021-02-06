using System;
using Microsoft.AspNetCore.Identity;
using PaymentManager.Api.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using PaymentManager.Api.Services.Interfaces;

namespace PaymentManager.Api.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _context;
        private readonly ISecurityCryptographyService _cryptographyService;
        private readonly Guid _webStoreId = Guid.Parse("54a0924b-200a-4efc-a6ac-ff21873c3b37");
        private readonly Guid _merchantId = Guid.Parse("5A25F89F-1294-426F-917C-08D8A515D437");

        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager, DataContext context, ISecurityCryptographyService cryptographyService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _context.Database.EnsureCreated();
            _cryptographyService = cryptographyService;
        }

        public void SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                var roles = new List<Role>
                {
                    new Role() {Name = "Admin"},
                    new Role() {Name = "User"}
                };

                foreach (var role in roles)
                {
                    _roleManager.CreateAsync(role).Wait();
                }

                var user = new User()
                {
                    UserName = "PublishingCompany",
                    Email = "admin@publishingcompany.net",
                    Name = "PublishingCompany"
                };

                _userManager.CreateAsync(user, "TestUser@123").Wait();
                _userManager.AddToRoleAsync(user, "User").Wait();

                var admin = new User { UserName = "Admin", Email = "admin@paymentmanager.com" };
                IdentityResult result = _userManager.CreateAsync(admin, "TestAdmin@123").Result;

                if (result.Succeeded)
                {
                    var adm = _userManager.FindByNameAsync("Admin").Result;
                    _userManager.AddToRoleAsync(adm, "Admin").Wait();
                }
            }
        }

        public void SeedPaymentServices()
        {
            if (!_context.PaymentServices.Any())
            {
                var payPalService = new PaymentService()
                {
                    Name = "PayPal",
                    Description = "PayPal service for handling all paypal transactions.",
                    Url = @"http://192.168.0.14:5006/paypal/",
                    IsPassTrough = false
                };

                _context.Add(payPalService);

                var bankService = new PaymentService()
                {
                    Name = "PaymentCard",
                    Description = "Payment service to handle all your payments with payment cards.",
                    Url = @"http://192.168.0.14:5006/bank/",
                    IsPassTrough = true
                };

                _context.Add(bankService);

                var bitcoinService = new PaymentService()
                {
                    Name = "BitCoin",
                    Description = "Payment service to handle all your crypto currency transactions.",
                    Url = @"http://192.168.0.14:5006/bitcoin/",
                    IsPassTrough = false
                };

                _context.Add(bitcoinService);

                _context.SaveChanges();
            }
        }

        public void SeedMerchant()
        {
            if (!_context.Merchants.Any())
            {
                var merchant = new Merchant
                {
                    Id = _merchantId,
                    MerchantUniqueId = "UniqueMerchantId123",
                    MerchantPassword = "UniqueMerchantPassword@123",
                    MerchantUniqueStoreId = $"PublishingCompany01Merchant3212"
                };

                merchant.MerchantUniqueId = _cryptographyService.EncryptStringAes(merchant.MerchantUniqueId);
                merchant.MerchantPassword = _cryptographyService.EncryptStringAes(merchant.MerchantPassword);

                var bitCoin = _context.PaymentServices.FirstOrDefault(ps => ps.Name == "BitCoin");
                var payPal = _context.PaymentServices.FirstOrDefault(ps => ps.Name == "PayPal");
                var bank = _context.PaymentServices.FirstOrDefault(ps => ps.Name == "PaymentCard");

                merchant.PaymentServices = new List<MerchantPaymentServices>()
                {
                    new MerchantPaymentServices()
                    {
                        PaymentService = bitCoin,
                        PaymentServiceId = bitCoin.Id,
                        Merchant = merchant
                    },
                    new MerchantPaymentServices()
                    {
                        PaymentService = bank,
                        PaymentServiceId = bank.Id,
                        Merchant = merchant
                    },
                    new MerchantPaymentServices()
                    {
                        PaymentService = payPal,
                        PaymentServiceId = payPal.Id,
                        Merchant = merchant
                    },
                };

                _context.Add(merchant);

                _context.SaveChanges();
            }
        }

        public void SeedWebStore()
        {
            if (!_context.WebStores.Any())
            {
                var webStore = new WebStore()
                {
                    Id = _webStoreId,
                    ErrorUrl = "http://localhost:3000/paymenterror",
                    FailureUrl = "http://localhost:3000/paymentfailure",
                    SuccessUrl = "http://localhost:3000/paymentsuccess",
                    SingleMerchantStore = true,
                    Url = "http://localhost:3000/home",
                    StoreName = "PublishingCompany01",
                    PaymentOptions = new List<WebStorePaymentService>()
                };

                var bitCoin = _context.PaymentServices.FirstOrDefault(ps => ps.Name == "BitCoin");
                var payPal = _context.PaymentServices.FirstOrDefault(ps => ps.Name == "PayPal");
                var bank = _context.PaymentServices.FirstOrDefault(ps => ps.Name == "PaymentCard");

                var merchant = _context.Merchants.FirstOrDefault(m => m.MerchantUniqueStoreId == "PublishingCompany01Merchant3212");

                webStore.PaymentOptions.Add(new WebStorePaymentService() { PaymentService = bitCoin, PaymentServiceId = bitCoin.Id, WebStore = webStore });
                webStore.PaymentOptions.Add(new WebStorePaymentService() { PaymentService = bank, PaymentServiceId = bank.Id, WebStore = webStore });
                webStore.PaymentOptions.Add(new WebStorePaymentService() { PaymentService = payPal, PaymentServiceId = payPal.Id, WebStore = webStore });

                webStore.Merchants = new List<Merchant>(){merchant};

                _context.Add(webStore);
                _context.SaveChanges();
                merchant.WebStore = webStore;
                _context.Update(merchant);
                _context.SaveChanges();
            }
        }
    }
}