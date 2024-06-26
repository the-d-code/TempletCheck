﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplicationLoan.Context;

#nullable disable

namespace WebApplicationLoan.Migrations
{
    [DbContext(typeof(InternalExamDbContext))]
    [Migration("20240406151208_createDb")]
    partial class createDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApplicationLoan.Models.Loan", b =>
                {
                    b.Property<int>("LoanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoanId"), 1L, 1);

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("InterestAmount")
                        .HasColumnType("float");

                    b.Property<double>("InterestRate")
                        .HasColumnType("float");

                    b.Property<int>("NoOfYears")
                        .HasColumnType("int");

                    b.Property<double>("PrincipalAmount")
                        .HasColumnType("float");

                    b.HasKey("LoanId");

                    b.ToTable("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
