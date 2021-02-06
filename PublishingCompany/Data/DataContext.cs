using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PublishingCompany.Api.Data.Entities;

namespace PublishingCompany.Api.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<UserBoughtBook> UserBoughtBooks { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserBoughtBook>(userBoughtBook =>
            {
                userBoughtBook.HasKey(ubb => new { ubb.UserId, ubb.BookId });

                userBoughtBook
                    .HasOne(ubb => ubb.Book)
                    .WithMany(book => book.UserBoughtBooks)
                    .HasForeignKey(ubb => ubb.BookId);

                userBoughtBook
                    .HasOne(ubb => ubb.User)
                    .WithMany(user => user.UserBoughtBooks)
                    .HasForeignKey(ubb => ubb.UserId);
            });

            builder.Entity<ShoppingCartItem>(shoppingCartItem =>
            {
                shoppingCartItem.HasKey(shc => new {shc.BookId, shc.ShoppingCartId});

                shoppingCartItem
                    .HasOne(shc => shc.ShoppingCart)
                    .WithMany(shc => shc.ShoppingCartItems)
                    .HasForeignKey(shc => shc.ShoppingCartId);
            });

            builder.Entity<OrderItem>(orderItem =>
            {
                orderItem.HasKey(odi => new { odi.BookId, odi.OrderId });

                orderItem
                    .HasOne(odi => odi.Order)
                    .WithMany(ord => ord.OrderItems)
                    .HasForeignKey(odi => odi.OrderId);
            });
        }
    }
}