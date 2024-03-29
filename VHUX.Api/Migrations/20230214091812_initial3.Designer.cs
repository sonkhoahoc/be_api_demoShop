﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VHUX.Api.Entity;

#nullable disable

namespace VHUX.Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230214091812_initial3")]
    partial class initial3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("VHUX.Api.Entity.Cart", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"), 1L, 1);

                    b.Property<long>("customer_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("dateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("is_delete")
                        .HasColumnType("bit");

                    b.Property<long>("userAdded")
                        .HasColumnType("bigint");

                    b.Property<long?>("userUpdated")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("cart");
                });

            modelBuilder.Entity("VHUX.Api.Entity.Cart_Detail", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"), 1L, 1);

                    b.Property<long>("cart_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("dateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("is_delete")
                        .HasColumnType("bit");

                    b.Property<long>("product_id")
                        .HasColumnType("bigint");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<long>("userAdded")
                        .HasColumnType("bigint");

                    b.Property<long?>("userUpdated")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("cart_detail");
                });

            modelBuilder.Entity("VHUX.Api.Entity.Category_News", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"), 1L, 1);

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("is_delete")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("userAdded")
                        .HasColumnType("bigint");

                    b.Property<long?>("userUpdated")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("category_news");
                });

            modelBuilder.Entity("VHUX.Api.Entity.Category_Product", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"), 1L, 1);

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("is_delete")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("userAdded")
                        .HasColumnType("bigint");

                    b.Property<long?>("userUpdated")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("category_product");
                });

            modelBuilder.Entity("VHUX.Api.Entity.Category_Size", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"), 1L, 1);

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("is_delete")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("userAdded")
                        .HasColumnType("bigint");

                    b.Property<long?>("userUpdated")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("category_size");
                });

            modelBuilder.Entity("VHUX.Api.Entity.Media_File", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"), 1L, 1);

                    b.Property<string>("file_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("idtable")
                        .HasColumnType("bigint");

                    b.Property<string>("ipserver")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<bool>("is_delete")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("name_guid")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tablename")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte>("type")
                        .HasColumnType("tinyint");

                    b.HasKey("id");

                    b.ToTable("media_file");
                });

            modelBuilder.Entity("VHUX.Api.Entity.News", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"), 1L, 1);

                    b.Property<DateTime>("dateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_delete")
                        .HasColumnType("bit");

                    b.Property<string>("meta_description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("meta_keywords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("meta_title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("userAdded")
                        .HasColumnType("bigint");

                    b.Property<long?>("userUpdated")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("news");
                });

            modelBuilder.Entity("VHUX.Api.Entity.Order", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"), 1L, 1);

                    b.Property<string>("customer_address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<long>("customer_dictrics_id")
                        .HasColumnType("bigint");

                    b.Property<string>("customer_dictrics_name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<long>("customer_id")
                        .HasColumnType("bigint");

                    b.Property<string>("customer_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_note")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("customer_phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("customer_provice_id")
                        .HasColumnType("bigint");

                    b.Property<string>("customer_provice_name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<long>("customer_wards_id")
                        .HasColumnType("bigint");

                    b.Property<string>("customer_wards_name")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("dateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("is_delete")
                        .HasColumnType("bit");

                    b.Property<double>("shiping_cost")
                        .HasColumnType("float");

                    b.Property<byte>("status")
                        .HasColumnType("tinyint");

                    b.Property<double>("total_price")
                        .HasColumnType("float");

                    b.Property<double>("total_product")
                        .HasColumnType("float");

                    b.Property<long>("userAdded")
                        .HasColumnType("bigint");

                    b.Property<long?>("userUpdated")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("order");
                });

            modelBuilder.Entity("VHUX.Api.Entity.Order_Detail", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"), 1L, 1);

                    b.Property<DateTime>("dateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("is_delete")
                        .HasColumnType("bit");

                    b.Property<long>("order_id")
                        .HasColumnType("bigint");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<long>("product_id")
                        .HasColumnType("bigint");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<long>("userAdded")
                        .HasColumnType("bigint");

                    b.Property<long?>("userUpdated")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("order_detail");
                });

            modelBuilder.Entity("VHUX.Api.Entity.Product", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"), 1L, 1);

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<long>("category_id")
                        .HasColumnType("bigint");

                    b.Property<long>("category_size_id")
                        .HasColumnType("bigint");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<bool>("is_delete")
                        .HasColumnType("bit");

                    b.Property<string>("meta_description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("meta_keywords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("meta_title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.Property<double>("price_sale")
                        .HasColumnType("float");

                    b.Property<int>("quantity_sold")
                        .HasColumnType("int");

                    b.Property<double>("ratio")
                        .HasColumnType("float");

                    b.Property<long>("userAdded")
                        .HasColumnType("bigint");

                    b.Property<long?>("userUpdated")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("product");
                });
#pragma warning restore 612, 618
        }
    }
}
