﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Soup.OrderSystem.Data;

#nullable disable

namespace Soup.OrderSystem.Data.Migrations
{
    [DbContext(typeof(OrderContext))]
    partial class OrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Soup.OrderSystem.Objects.Customer.Address", b =>
                {
                    b.Property<int>("Customer_Id")
                        .HasColumnType("int");

                    b.Property<int?>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<int>("Postal_Code")
                        .HasColumnType("int");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Soup.OrderSystem.Objects.Customer.CustomerDetails", b =>
                {
                    b.Property<string>("Customer_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("CustomerDetails");
                });

            modelBuilder.Entity("Soup.OrderSystem.Objects.Customer.CustomerId", b =>
                {
                    b.Property<string>("Customer_Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Customer_Id");

                    b.ToTable("CustomerIds");
                });

            modelBuilder.Entity("Soup.OrderSystem.Objects.Customer.PostalCode", b =>
                {
                    b.Property<int>("Postal_Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Postal_Code"));

                    b.Property<string>("Municipality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Postal_Code");

                    b.ToTable("PostalCodes");
                });

            modelBuilder.Entity("Soup.OrderSystem.Objects.Order.OrderDetails", b =>
                {
                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<int>("Order_Id")
                        .HasColumnType("int");

                    b.Property<int>("ProductAmount")
                        .HasColumnType("int");

                    b.Property<int>("Product_Id")
                        .HasColumnType("int");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Soup.OrderSystem.Objects.Order.OrderId", b =>
                {
                    b.Property<int>("Order_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Order_Id"));

                    b.Property<int>("Customer_Id")
                        .HasColumnType("int");

                    b.HasKey("Order_Id");

                    b.ToTable("OrderIds");
                });

            modelBuilder.Entity("Soup.OrderSystem.Objects.Product.Product", b =>
                {
                    b.Property<int>("Product_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Product_Id"));

                    b.Property<string>("Product_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockAmount")
                        .HasColumnType("int");

                    b.HasKey("Product_Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Soup.OrderSystem.Objects.User.UserDetails", b =>
                {
                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("Soup.OrderSystem.Objects.User.UserId", b =>
                {
                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_Id"));

                    b.HasKey("User_Id");

                    b.ToTable("UserIds");
                });
#pragma warning restore 612, 618
        }
    }
}
