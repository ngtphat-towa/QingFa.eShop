using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qingfa.eShop.Infrastructure.Migrations
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
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
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
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
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
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExampleMetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    BrandId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PriceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price_Currency = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
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
                name: "IX_CategoryProduct_ProductId",
                table: "CategoryProduct",
                column: "ProductId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "ExampleMetas");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
