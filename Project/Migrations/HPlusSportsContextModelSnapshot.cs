﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Project.EFClasses;
using System;

namespace Project.Migrations
{
    [DbContext(typeof(HPlusSportsContext))]
    partial class HPlusSportsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-preview1-24937")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project.EFClasses.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CustomerID");

                    b.Property<string>("CmpLastFirst")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("cmp_LastFirst")
                        .HasColumnType("varchar(102)")
                        .HasComputedColumnSql("(([str_fld_LastName]+', ')+[str_fld_FirstName])");

                    b.Property<string>("StrFldAddress")
                        .HasColumnName("str_fld_Address")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StrFldCity")
                        .HasColumnName("str_fld_City")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StrFldEmail")
                        .HasColumnName("str_fld_Email")
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("BackingField", "_email");

                    b.Property<string>("StrFldFirstName")
                        .HasColumnName("str_fld_FirstName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StrFldLastName")
                        .HasColumnName("str_fld_LastName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StrFldPhone")
                        .HasColumnName("str_fld_Phone")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StrFldState")
                        .HasColumnName("str_fld_State")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("StrFldZipcode")
                        .HasColumnName("str_fld_Zipcode")
                        .HasColumnType("varchar(50)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Project.EFClasses.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OrderID");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("CustomerId")
                        .HasColumnName("CustomerID");

                    b.Property<byte[]>("LastUpdate")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime");

                    b.Property<int>("SalespersonId")
                        .HasColumnName("SalespersonID");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(50)")
                        .HasDefaultValueSql("('none')");

                    b.Property<decimal?>("TotalDue")
                        .HasColumnType("money");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderDate")
                        .HasName("IX_Order");

                    b.HasIndex("SalespersonId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Project.EFClasses.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OrderItemID");

                    b.Property<int>("OrderId")
                        .HasColumnName("OrderID");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnName("ProductID")
                        .HasMaxLength(10);

                    b.Property<int?>("Quantity");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("Project.EFClasses.Product", b =>
                {
                    b.Property<string>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProductID")
                        .HasMaxLength(10);

                    b.Property<int?>("ExpirationDays");

                    b.Property<bool>("Perishable")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((0))");

                    b.Property<decimal?>("Price")
                        .HasColumnType("money");

                    b.Property<string>("ProductName")
                        .HasColumnType("varchar(50)");

                    b.Property<bool?>("Refrigerated");

                    b.Property<int?>("Size");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Variety")
                        .HasColumnType("varchar(50)");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Project.EFClasses.SalesGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("State", "Type")
                        .IsUnique()
                        .HasName("IX_StateType");

                    b.ToTable("SalesGroup");
                });

            modelBuilder.Entity("Project.EFClasses.Salesperson", b =>
                {
                    b.Property<int>("SalespersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SalespersonID");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("City")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("SalesGroupState")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(N'CA')")
                        .HasMaxLength(2);

                    b.Property<int>("SalesGroupType")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("State")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Zipcode")
                        .HasColumnType("varchar(50)");

                    b.HasKey("SalespersonId");

                    b.ToTable("Salesperson");
                });

            modelBuilder.Entity("Project.EFClasses.Order", b =>
                {
                    b.HasOne("Project.EFClasses.Customer", "Customer")
                        .WithMany("Order")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Order_Customer");

                    b.HasOne("Project.EFClasses.Salesperson", "Salesperson")
                        .WithMany("Order")
                        .HasForeignKey("SalespersonId")
                        .HasConstraintName("FK_Order_Salesperson");
                });

            modelBuilder.Entity("Project.EFClasses.OrderItem", b =>
                {
                    b.HasOne("Project.EFClasses.Order", "Order")
                        .WithMany("OrderItem")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_OrderItem_Order");

                    b.HasOne("Project.EFClasses.Product", "Product")
                        .WithMany("OrderItem")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_OrderItem_Product1");
                });

            modelBuilder.Entity("Project.EFClasses.Salesperson", b =>
                {
                    b.HasOne("Project.EFClasses.Salesperson", "SalespersonNavigation")
                        .WithOne("InverseSalespersonNavigation")
                        .HasForeignKey("Project.EFClasses.Salesperson", "SalespersonId")
                        .HasConstraintName("FK_Salesperson_Salesperson");
                });
        }
    }
}
