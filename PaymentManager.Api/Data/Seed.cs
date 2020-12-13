using Microsoft.AspNetCore.Identity;
using PaymentManager.Api.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;

namespace PaymentManager.Api.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly DataContext _context;

        public Seed(UserManager<User> userManager, RoleManager<Role> roleManager, DataContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _context.Database.EnsureCreated();
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
                    Url = @"https://localhost:5005/paypal/"
                };

                _context.Add(payPalService);

                var bankService = new PaymentService()
                {
                    Name = "PaymentCard",
                    Description = "Payment service to handle all your payments with payment cards.",
                    Url = @"https://localhost:5005/bank/"
                };

                _context.Add(bankService);

                var bitcoinService = new PaymentService()
                {
                    Name = "BitCoin",
                    Description = "Payment service to handle all your crypto currency transactions.",
                    Url = @"https://localhost:5005/bitcoin/"
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
                    MerchantUniqueId = "UniqueMerchantId123",
                    MerchantPassword = "UniqueMerchantPassword@123"
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
                    ErrorUrl = "http://localhost:3000/paymenterror",
                    FailureUrl = "http://localhost:3000/paymentfailure",
                    SuccessUrl = "http://localhost:3000/paymentsuccess",
                    SingleMerchantStore = true,
                    Url = "http://localhost:3000/home",
                    PaymentOptions = new List<WebStorePaymentService>()
                };

                var bitCoin = _context.PaymentServices.FirstOrDefault(ps => ps.Name == "BitCoin");
                var payPal = _context.PaymentServices.FirstOrDefault(ps => ps.Name == "PayPal");
                var bank = _context.PaymentServices.FirstOrDefault(ps => ps.Name == "PaymentCard");

                var merchant = _context.Merchants.FirstOrDefault(m => m.MerchantUniqueId == "UniqueMerchantId123");
                merchant.WebStore = webStore;

                webStore.PaymentOptions.Add(new WebStorePaymentService() { PaymentService = bitCoin, PaymentServiceId = bitCoin.Id, WebStore = webStore });
                webStore.PaymentOptions.Add(new WebStorePaymentService() { PaymentService = bank, PaymentServiceId = bank.Id, WebStore = webStore });
                webStore.PaymentOptions.Add(new WebStorePaymentService() { PaymentService = payPal, PaymentServiceId = payPal.Id, WebStore = webStore });

                webStore.Merchants = new List<Merchant>(){merchant};

                _context.Add(webStore);

                _context.SaveChanges();
            }
        }
    }
}