using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentManager.Api.Data.Entities;

namespace PaymentManager.Api.Data
{
    public class DataContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>,
        UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<PaymentService> PaymentServices { get; set; }
        public DbSet<WebStore> WebStores { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PaymentRequest> PaymentRequests { get; set; }
        public DbSet<WebStorePaymentService> WebStorePaymentServices { get; set; }
        public DbSet<MerchantPaymentServices> MerchantPaymentServices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<WebStorePaymentService>(webStorePaymentService =>
            {
                webStorePaymentService.HasKey(wsps => new {wsps.PaymentServiceId, wsps.WebStoreId});

                webStorePaymentService
                    .HasOne(wsp => wsp.PaymentService)
                    .WithMany(ps => ps.WebStores)
                    .HasForeignKey(wsp => wsp.PaymentServiceId);

                webStorePaymentService
                    .HasOne(wsp => wsp.WebStore)
                    .WithMany(ws => ws.PaymentOptions)
                    .HasForeignKey(wsp => wsp.WebStoreId);
            });

            builder.Entity<MerchantPaymentServices>(merchantPaymentService =>
            {
                merchantPaymentService.HasKey(merchps => new { merchps.PaymentServiceId, merchps.MerchantId });

                merchantPaymentService
                    .HasOne(msp => msp.Merchant)
                    .WithMany(ms => ms.PaymentServices)
                    .HasForeignKey(msp => msp.MerchantId);
            });

            builder.Entity<WebStore>(webStore =>
            {
                webStore
                    .HasMany(ws => ws.Merchants)
                    .WithOne(m => m.WebStore);
            });

            builder.Entity<Merchant>(merchant => { merchant.HasMany(me => me.PaymentServices); });
        }
    }
}