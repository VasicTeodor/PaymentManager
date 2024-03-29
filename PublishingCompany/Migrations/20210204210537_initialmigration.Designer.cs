﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PublishingCompany.Api.Data;

namespace PublishingCompany.Api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210204210537_initialmigration")]
    partial class initialmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PublishingCompany.Api.Data.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Isbn")
                        .HasColumnType("bigint");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("int");

                    b.Property<string>("PlaceOfPublishing")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("Publisher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Synopsis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("TableVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Writers")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("PublishingCompany.Api.Data.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("OrderPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<byte[]>("TableVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PublishingCompany.Api.Data.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("TableVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("BookId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("PublishingCompany.Api.Data.Entities.ShoppingCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("TableVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("PublishingCompany.Api.Data.Entities.ShoppingCartItem", b =>
                {
                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShoppingCartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("TableVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("BookId", "ShoppingCartId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("PublishingCompany.Api.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDateOfSubscription")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBetaReader")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDateOfSubscription")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Subscribed")
                        .HasColumnType("bit");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("TableVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("UserRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PublishingCompany.Api.Data.Entities.UserBoughtBook", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("TableVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("UserId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("UserBoughtBooks");
                });

            modelBuilder.Entity("PublishingCompany.Api.Data.Entities.OrderItem", b =>
                {
                    b.HasOne("PublishingCompany.Api.Data.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PublishingCompany.Api.Data.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PublishingCompany.Api.Data.Entities.ShoppingCart", b =>
                {
                    b.HasOne("PublishingCompany.Api.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("PublishingCompany.Api.Data.Entities.ShoppingCartItem", b =>
                {
                    b.HasOne("PublishingCompany.Api.Data.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PublishingCompany.Api.Data.Entities.ShoppingCart", "ShoppingCart")
                        .WithMany("ShoppingCartItems")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PublishingCompany.Api.Data.Entities.UserBoughtBook", b =>
                {
                    b.HasOne("PublishingCompany.Api.Data.Entities.Book", "Book")
                        .WithMany("UserBoughtBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PublishingCompany.Api.Data.Entities.User", "User")
                        .WithMany("UserBoughtBooks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
