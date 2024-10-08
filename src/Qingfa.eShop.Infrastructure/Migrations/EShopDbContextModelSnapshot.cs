﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QingFa.EShop.Infrastructure.Persistence;

#nullable disable

namespace Qingfa.eShop.Infrastructure.Migrations
{
    [DbContext(typeof(EShopDbContext))]
    partial class EShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Attributes.AttributeGroup", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GroupName")
                        .HasDatabaseName("IX_AttributeGroups_GroupName");

                    b.ToTable("AttributeGroups", (string)null);
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Attributes.AttributeOption", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<int>("AttributeId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("AttributeId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OptionValue")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<int>("SortOrder")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("OptionValue")
                        .HasDatabaseName("IX_AttributeOptions_OptionValue");

                    b.ToTable("AttributeOptions", (string)null);
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Attributes.ProductAttribute", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("AttributeCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("AttributeGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFilterable")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<bool>("ShowToCustomers")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SortOrder")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AttributeCode")
                        .HasDatabaseName("IX_Attributes_AttributeCode");

                    b.HasIndex("AttributeGroupId");

                    b.ToTable("Attributes", (string)null);
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Brands.Brand", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<short>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("INTEGER");

                    b.ComplexProperty<Dictionary<string, object>>("Seo", "QingFa.EShop.Domain.Catalogs.Brands.Brand.Seo#SeoInfo", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("CanonicalURL")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("TEXT");

                            b1.Property<string>("MetaDescription")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("TEXT");

                            b1.Property<string>("MetaTitle")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("TEXT");

                            b1.Property<string>("URLSlug")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT");
                        });

                    b.HasKey("Id");

                    b.HasIndex("LogoUrl")
                        .HasDatabaseName("IX_Brands_LogoUrl");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_Brands_Name");

                    b.ToTable("Brands", (string)null);
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Categories.Category", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BannerURL")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IncludeToStore")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentId")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("INTEGER");

                    b.ComplexProperty<Dictionary<string, object>>("Seo", "QingFa.EShop.Domain.Catalogs.Categories.Category.Seo#SeoInfo", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("CanonicalURL")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("TEXT");

                            b1.Property<string>("MetaDescription")
                                .IsRequired()
                                .HasMaxLength(500)
                                .HasColumnType("TEXT");

                            b1.Property<string>("MetaTitle")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("TEXT");

                            b1.Property<string>("URLSlug")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("TEXT");
                        });

                    b.HasKey("Id");

                    b.HasIndex("BannerURL")
                        .HasDatabaseName("IX_Categories_BannerURL");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_Categories_Name");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<int>("BrandId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("BrandId");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("CategoryId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT")
                        .HasColumnName("Description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Price");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("INTEGER")
                        .HasColumnName("StockQuantity");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_Products_Name");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Variants.ProductVariant", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsActive");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Price");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("SKU");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("INTEGER")
                        .HasColumnName("StockQuantity");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SKU")
                        .IsUnique()
                        .HasDatabaseName("IX_ProductVariants_SKU");

                    b.ToTable("ProductVariants", (string)null);
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Variants.ProductVariantAttribute", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<int>("AttributeId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("AttributeId");

                    b.Property<int?>("AttributeOptionId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("AttributeOptionId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomValue")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("CustomValue");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsRequired");

                    b.Property<bool>("IsVisibleToCustomer")
                        .HasColumnType("INTEGER")
                        .HasColumnName("IsVisibleToCustomer");

                    b.Property<int>("ProductVariantId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ProductVariantId");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("AttributeOptionId");

                    b.HasIndex("ProductVariantId", "AttributeId")
                        .HasDatabaseName("IX_ProductVariantAttributes_ProductVariantId_AttributeId");

                    b.ToTable("ProductVariantAttributes", (string)null);
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Attributes.AttributeOption", b =>
                {
                    b.HasOne("QingFa.EShop.Domain.Catalogs.Attributes.ProductAttribute", null)
                        .WithMany("Options")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Attributes.ProductAttribute", b =>
                {
                    b.HasOne("QingFa.EShop.Domain.Catalogs.Attributes.AttributeGroup", null)
                        .WithMany()
                        .HasForeignKey("AttributeGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Categories.Category", b =>
                {
                    b.HasOne("QingFa.EShop.Domain.Catalogs.Categories.Category", null)
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Products.Product", b =>
                {
                    b.HasOne("QingFa.EShop.Domain.Catalogs.Brands.Brand", null)
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QingFa.EShop.Domain.Catalogs.Categories.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Variants.ProductVariant", b =>
                {
                    b.HasOne("QingFa.EShop.Domain.Catalogs.Products.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Variants.ProductVariantAttribute", b =>
                {
                    b.HasOne("QingFa.EShop.Domain.Catalogs.Attributes.ProductAttribute", "Attribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QingFa.EShop.Domain.Catalogs.Attributes.AttributeOption", "AttributeOption")
                        .WithMany()
                        .HasForeignKey("AttributeOptionId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("QingFa.EShop.Domain.Catalogs.Variants.ProductVariant", "ProductVariant")
                        .WithMany("ProductVariantAttributes")
                        .HasForeignKey("ProductVariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("AttributeOption");

                    b.Navigation("ProductVariant");
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Attributes.ProductAttribute", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("QingFa.EShop.Domain.Catalogs.Variants.ProductVariant", b =>
                {
                    b.Navigation("ProductVariantAttributes");
                });
#pragma warning restore 612, 618
        }
    }
}
