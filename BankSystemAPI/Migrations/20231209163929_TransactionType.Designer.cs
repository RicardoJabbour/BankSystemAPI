﻿// <auto-generated />
using System;
using BankSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankSystemAPI.Migrations
{
    [DbContext(typeof(BankDbContext))]
    [Migration("20231209163929_TransactionType")]
    partial class TransactionType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankSystemAPI.Data.Models.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("AccountId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BankSystemAPI.Data.Models.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BankSystemAPI.Data.Models.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BankSystemAPI.Data.Models.Entities.Account", b =>
                {
                    b.HasOne("BankSystemAPI.Data.Models.Entities.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BankSystemAPI.Data.Models.Entities.Transaction", b =>
                {
                    b.HasOne("BankSystemAPI.Data.Models.Entities.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BankSystemAPI.Data.Models.Entities.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BankSystemAPI.Data.Models.Entities.Customer", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}