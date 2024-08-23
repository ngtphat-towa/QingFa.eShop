using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QingFa.EShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init_Catalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    SeoMeta_Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SeoMeta_Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    SeoMeta_Keywords = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    SeoMeta_CanonicalUrl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    SeoMeta_Robots = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    LogoUrl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    SeoMeta_Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SeoMeta_Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    SeoMeta_Keywords = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    SeoMeta_CanonicalUrl = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    SeoMeta_Robots = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ParentCategoryId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExampleMetas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExampleMetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 400, nullable: true),
                    LongDescription = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    SKU = table.Column<string>(type: "TEXT", nullable: false),
                    StockLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    BrandId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PriceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price_Currency = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    AttributeCode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    IsRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsFilterable = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShowToCustomers = table.Column<bool>(type: "INTEGER", nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    AttributeGroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_ProductAttributeGroup_AttributeGroupId",
                        column: x => x.AttributeGroupId,
                        principalTable: "ProductAttributeGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SKU = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    StockLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    VariantStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    Price_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price_Currency = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariant_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributeOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OptionValue = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductAttributeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributeOption_ProductAttribute_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariantAttribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductVariantId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductAttributeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductAttributeOptionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    IsRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsVisibleToCustomer = table.Column<bool>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariantAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariantAttribute_ProductAttributeOption_ProductAttributeOptionId",
                        column: x => x.ProductAttributeOptionId,
                        principalTable: "ProductAttributeOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariantAttribute_ProductAttribute_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariantAttribute_ProductVariant_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Created",
                table: "Brands",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CreatedBy",
                table: "Brands",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_LastModified",
                table: "Brands",
                column: "LastModified");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_LastModifiedBy",
                table: "Brands",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_SeoMeta_Description",
                table: "Brands",
                column: "SeoMeta_Description");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_SeoMeta_Keywords",
                table: "Brands",
                column: "SeoMeta_Keywords");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_SeoMeta_Title",
                table: "Brands",
                column: "SeoMeta_Title");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Status",
                table: "Brands",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Created",
                table: "Categories",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedBy",
                table: "Categories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LastModified",
                table: "Categories",
                column: "LastModified");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LastModifiedBy",
                table: "Categories",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SeoMeta_Description",
                table: "Categories",
                column: "SeoMeta_Description");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SeoMeta_Keywords",
                table: "Categories",
                column: "SeoMeta_Keywords");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SeoMeta_Title",
                table: "Categories",
                column: "SeoMeta_Title");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Status",
                table: "Categories",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductId",
                table: "CategoryProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ExampleMetas_Created",
                table: "ExampleMetas",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_ExampleMetas_CreatedBy",
                table: "ExampleMetas",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ExampleMetas_LastModified",
                table: "ExampleMetas",
                column: "LastModified");

            migrationBuilder.CreateIndex(
                name: "IX_ExampleMetas_LastModifiedBy",
                table: "ExampleMetas",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ExampleMetas_Status",
                table: "ExampleMetas",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_AttributeCode",
                table: "ProductAttribute",
                column: "AttributeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_AttributeGroupId",
                table: "ProductAttribute",
                column: "AttributeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_Created",
                table: "ProductAttribute",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_CreatedBy",
                table: "ProductAttribute",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_LastModified",
                table: "ProductAttribute",
                column: "LastModified");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_LastModifiedBy",
                table: "ProductAttribute",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_Status",
                table: "ProductAttribute",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeGroup_Created",
                table: "ProductAttributeGroup",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeGroup_CreatedBy",
                table: "ProductAttributeGroup",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeGroup_LastModified",
                table: "ProductAttributeGroup",
                column: "LastModified");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeGroup_LastModifiedBy",
                table: "ProductAttributeGroup",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeGroup_Status",
                table: "ProductAttributeGroup",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeOption_Created",
                table: "ProductAttributeOption",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeOption_CreatedBy",
                table: "ProductAttributeOption",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeOption_LastModified",
                table: "ProductAttributeOption",
                column: "LastModified");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeOption_LastModifiedBy",
                table: "ProductAttributeOption",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeOption_ProductAttributeId",
                table: "ProductAttributeOption",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeOption_Status",
                table: "ProductAttributeOption",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Created",
                table: "Products",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedBy",
                table: "Products",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_LastModified",
                table: "Products",
                column: "LastModified");

            migrationBuilder.CreateIndex(
                name: "IX_Products_LastModifiedBy",
                table: "Products",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Status",
                table: "Products",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_Created",
                table: "ProductVariant",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_CreatedBy",
                table: "ProductVariant",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_LastModified",
                table: "ProductVariant",
                column: "LastModified");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_LastModifiedBy",
                table: "ProductVariant",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_ProductId",
                table: "ProductVariant",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariant_Status",
                table: "ProductVariant",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantAttribute_Created",
                table: "ProductVariantAttribute",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantAttribute_CreatedBy",
                table: "ProductVariantAttribute",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantAttribute_LastModified",
                table: "ProductVariantAttribute",
                column: "LastModified");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantAttribute_LastModifiedBy",
                table: "ProductVariantAttribute",
                column: "LastModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantAttribute_ProductAttributeId",
                table: "ProductVariantAttribute",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantAttribute_ProductAttributeOptionId",
                table: "ProductVariantAttribute",
                column: "ProductAttributeOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantAttribute_ProductVariantId_ProductAttributeId_ProductAttributeOptionId",
                table: "ProductVariantAttribute",
                columns: new[] { "ProductVariantId", "ProductAttributeId", "ProductAttributeOptionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantAttribute_Status",
                table: "ProductVariantAttribute",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "ExampleMetas");

            migrationBuilder.DropTable(
                name: "ProductVariantAttribute");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ProductAttributeOption");

            migrationBuilder.DropTable(
                name: "ProductVariant");

            migrationBuilder.DropTable(
                name: "ProductAttribute");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductAttributeGroup");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
