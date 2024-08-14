using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QingFa.eShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    SocialSharingEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    IsReturnable = table.Column<bool>(type: "boolean", nullable: false),
                    IsExchangeable = table.Column<bool>(type: "boolean", nullable: false),
                    PickupEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    IsTryAndBuyEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    ServiceabilityDisclaimer_Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ServiceabilityDisclaimer_Desc = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogBrands",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Bio = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LogoURL = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    WebsiteURL = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CountryOfOrigin = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EstablishmentYear = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    ImageType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Resolutions = table.Column<string>(type: "jsonb", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SizeOptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SizeLabel = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MinimumValue = table.Column<int>(type: "int", nullable: true),
                    MaximumValue = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    InventoryCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StyleImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SizeRepresentationUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    DefaultImageId = table.Column<int>(type: "integer", nullable: false),
                    SearchImageId = table.Column<int>(type: "integer", nullable: false),
                    BackImageId = table.Column<int>(type: "integer", nullable: false),
                    FrontImageId = table.Column<int>(type: "integer", nullable: false),
                    RightImageId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    UpdatedBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StyleImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StyleImage_ImageData_BackImageId",
                        column: x => x.BackImageId,
                        principalTable: "ImageData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StyleImage_ImageData_DefaultImageId",
                        column: x => x.DefaultImageId,
                        principalTable: "ImageData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StyleImage_ImageData_FrontImageId",
                        column: x => x.FrontImageId,
                        principalTable: "ImageData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StyleImage_ImageData_RightImageId",
                        column: x => x.RightImageId,
                        principalTable: "ImageData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StyleImage_ImageData_SearchImageId",
                        column: x => x.SearchImageId,
                        principalTable: "ImageData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StyleImage_BackImageId",
                table: "StyleImage",
                column: "BackImageId");

            migrationBuilder.CreateIndex(
                name: "IX_StyleImage_DefaultImageId",
                table: "StyleImage",
                column: "DefaultImageId");

            migrationBuilder.CreateIndex(
                name: "IX_StyleImage_FrontImageId",
                table: "StyleImage",
                column: "FrontImageId");

            migrationBuilder.CreateIndex(
                name: "IX_StyleImage_RightImageId",
                table: "StyleImage",
                column: "RightImageId");

            migrationBuilder.CreateIndex(
                name: "IX_StyleImage_SearchImageId",
                table: "StyleImage",
                column: "SearchImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleTypes");

            migrationBuilder.DropTable(
                name: "CatalogBrands");

            migrationBuilder.DropTable(
                name: "SizeOptions");

            migrationBuilder.DropTable(
                name: "StyleImage");

            migrationBuilder.DropTable(
                name: "ImageData");
        }
    }
}
