using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qingfa.eShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    LogoUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    Seo_CanonicalURL = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Seo_MetaDescription = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Seo_MetaTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Seo_URLSlug = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedBy = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogAttributeGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<short>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedBy = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogAttributeGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    AttributeCode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<short>(type: "INTEGER", nullable: false),
                    IsRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsFilterable = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShowToCustomers = table.Column<bool>(type: "INTEGER", nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    Status = table.Column<short>(type: "INTEGER", nullable: false),
                    GroupAttributeId = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedBy = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ShortDescription = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    BrandId = table.Column<int>(type: "INTEGER", nullable: false),
                    SKU = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    StockLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    Price_Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Price_Currency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedBy = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    BannerURL = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Status = table.Column<short>(type: "INTEGER", nullable: false),
                    IncludeToStore = table.Column<bool>(type: "INTEGER", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                    Seo_CanonicalURL = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Seo_MetaDescription = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Seo_MetaTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Seo_URLSlug = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedBy = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttributeOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    AttributeId = table.Column<int>(type: "INTEGER", nullable: false),
                    OptionValue = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedBy = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeOptions_CatalogAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "CatalogAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    CatalogItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    VariantId = table.Column<int>(type: "INTEGER", nullable: true),
                    AttributeId = table.Column<int>(type: "INTEGER", nullable: false),
                    OptionId = table.Column<int>(type: "INTEGER", nullable: true),
                    Value = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    IsVisibleToCustomer = table.Column<bool>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedBy = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_AttributeOptions_OptionId",
                        column: x => x.OptionId,
                        principalTable: "AttributeOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_CatalogAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "CatalogAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributeValues_CatalogItems_CatalogItemId",
                        column: x => x.CatalogItemId,
                        principalTable: "CatalogItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeOptions_AttributeId",
                table: "AttributeOptions",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeOptions_AttributeId_IsDefault",
                table: "AttributeOptions",
                columns: new[] { "AttributeId", "IsDefault" });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeOptions_OptionValue",
                table: "AttributeOptions",
                column: "OptionValue");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_LogoUrl",
                table: "Brands",
                column: "LogoUrl");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogAttributeGroups_Name",
                table: "CatalogAttributeGroups",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogAttributeGroups_SortOrder",
                table: "CatalogAttributeGroups",
                column: "SortOrder");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogAttributes_AttributeCode",
                table: "CatalogAttributes",
                column: "AttributeCode");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogAttributes_Name",
                table: "CatalogAttributes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogAttributes_Type_Status",
                table: "CatalogAttributes",
                columns: new[] { "Type", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_SKU",
                table: "CatalogItems",
                column: "SKU");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_IncludeToStore",
                table: "Categories",
                column: "IncludeToStore");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Status",
                table: "Categories",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogAttributeValues_AttributeId",
                table: "ProductAttributeValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogAttributeValues_CatalogItemId",
                table: "ProductAttributeValues",
                column: "CatalogItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeValues_OptionId",
                table: "ProductAttributeValues",
                column: "OptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "CatalogAttributeGroups");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ProductAttributeValues");

            migrationBuilder.DropTable(
                name: "AttributeOptions");

            migrationBuilder.DropTable(
                name: "CatalogItems");

            migrationBuilder.DropTable(
                name: "CatalogAttributes");
        }
    }
}
