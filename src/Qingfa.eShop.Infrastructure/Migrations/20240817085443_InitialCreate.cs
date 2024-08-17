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
                    Status = table.Column<short>(type: "INTEGER", nullable: false),
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
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    BannerURL = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    IncludeToStore = table.Column<bool>(type: "INTEGER", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                    Seo_MetaTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Seo_MetaDescription = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Seo_URLSlug = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Seo_CanonicalURL = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_Brands_LogoUrl",
                table: "Brands",
                column: "LogoUrl");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_BannerURL",
                table: "Categories",
                column: "BannerURL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
