using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qingfa.eShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Catalog_Attribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VariantAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AttributeCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    IsRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsFilterable = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShowToCustomers = table.Column<bool>(type: "INTEGER", nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: true),
                    UpdatedBy = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    AttributeId = table.Column<int>(type: "INTEGER", nullable: false),
                    OptionValue = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
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
                        name: "FK_AttributeOptions_VariantAttributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "VariantAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeOptions_AttributeId",
                table: "AttributeOptions",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeOptions_OptionValue",
                table: "AttributeOptions",
                column: "OptionValue");

            migrationBuilder.CreateIndex(
                name: "IX_VariantAttributes_AttributeCode",
                table: "VariantAttributes",
                column: "AttributeCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeOptions");

            migrationBuilder.DropTable(
                name: "VariantAttributes");
        }
    }
}
