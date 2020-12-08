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
        public DbSet<WebStoreMerchant> WebStoreMerchants { get; set; }
        public DbSet<WebStorePaymentService> WebStorePaymentServices { get; set; }

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

            builder.Entity<WebStoreMerchant>(webStoreMerchant =>
            {
                webStoreMerchant.HasKey(wsm => new {wsm.WebStoreId, wsm.MerchantId});
            });

            builder.Entity<WebStorePaymentService>(webStorePaymentService =>
            {
                webStorePaymentService.HasKey(wsps => new {wsps.PaymentServiceId, wsps.WebStoreId});
            });
        }
    }
}