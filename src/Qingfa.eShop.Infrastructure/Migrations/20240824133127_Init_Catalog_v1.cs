using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qingfa.eShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init_Catalog_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantAttribute_ProductAttributeOptions_ProductAttributeOptionId",
                table: "ProductVariantAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantAttribute_ProductAttributes_ProductAttributeId",
                table: "ProductVariantAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantAttribute_ProductVariant_ProductVariantId",
                table: "ProductVariantAttribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantAttribute",
                table: "ProductVariantAttribute");

            migrationBuilder.RenameTable(
                name: "ProductVariantAttribute",
                newName: "ProductVariantAttributes");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttribute_Status",
                table: "ProductVariantAttributes",
                newName: "IX_ProductVariantAttributes_Status");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttribute_ProductVariantId_ProductAttributeId_ProductAttributeOptionId",
                table: "ProductVariantAttributes",
                newName: "IX_ProductVariantAttributes_ProductVariantId_ProductAttributeId_ProductAttributeOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttribute_ProductAttributeOptionId",
                table: "ProductVariantAttributes",
                newName: "IX_ProductVariantAttributes_ProductAttributeOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttribute_ProductAttributeId",
                table: "ProductVariantAttributes",
                newName: "IX_ProductVariantAttributes_ProductAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttribute_LastModifiedBy",
                table: "ProductVariantAttributes",
                newName: "IX_ProductVariantAttributes_LastModifiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttribute_LastModified",
                table: "ProductVariantAttributes",
                newName: "IX_ProductVariantAttributes_LastModified");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttribute_CreatedBy",
                table: "ProductVariantAttributes",
                newName: "IX_ProductVariantAttributes_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttribute_Created",
                table: "ProductVariantAttributes",
                newName: "IX_ProductVariantAttributes_Created");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantAttributes",
                table: "ProductVariantAttributes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantAttributes_ProductAttributeOptions_ProductAttributeOptionId",
                table: "ProductVariantAttributes",
                column: "ProductAttributeOptionId",
                principalTable: "ProductAttributeOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantAttributes_ProductAttributes_ProductAttributeId",
                table: "ProductVariantAttributes",
                column: "ProductAttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantAttributes_ProductVariant_ProductVariantId",
                table: "ProductVariantAttributes",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantAttributes_ProductAttributeOptions_ProductAttributeOptionId",
                table: "ProductVariantAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantAttributes_ProductAttributes_ProductAttributeId",
                table: "ProductVariantAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantAttributes_ProductVariant_ProductVariantId",
                table: "ProductVariantAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantAttributes",
                table: "ProductVariantAttributes");

            migrationBuilder.RenameTable(
                name: "ProductVariantAttributes",
                newName: "ProductVariantAttribute");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttributes_Status",
                table: "ProductVariantAttribute",
                newName: "IX_ProductVariantAttribute_Status");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttributes_ProductVariantId_ProductAttributeId_ProductAttributeOptionId",
                table: "ProductVariantAttribute",
                newName: "IX_ProductVariantAttribute_ProductVariantId_ProductAttributeId_ProductAttributeOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttributes_ProductAttributeOptionId",
                table: "ProductVariantAttribute",
                newName: "IX_ProductVariantAttribute_ProductAttributeOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttributes_ProductAttributeId",
                table: "ProductVariantAttribute",
                newName: "IX_ProductVariantAttribute_ProductAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttributes_LastModifiedBy",
                table: "ProductVariantAttribute",
                newName: "IX_ProductVariantAttribute_LastModifiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttributes_LastModified",
                table: "ProductVariantAttribute",
                newName: "IX_ProductVariantAttribute_LastModified");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttributes_CreatedBy",
                table: "ProductVariantAttribute",
                newName: "IX_ProductVariantAttribute_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantAttributes_Created",
                table: "ProductVariantAttribute",
                newName: "IX_ProductVariantAttribute_Created");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantAttribute",
                table: "ProductVariantAttribute",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantAttribute_ProductAttributeOptions_ProductAttributeOptionId",
                table: "ProductVariantAttribute",
                column: "ProductAttributeOptionId",
                principalTable: "ProductAttributeOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantAttribute_ProductAttributes_ProductAttributeId",
                table: "ProductVariantAttribute",
                column: "ProductAttributeId",
                principalTable: "ProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantAttribute_ProductVariant_ProductVariantId",
                table: "ProductVariantAttribute",
                column: "ProductVariantId",
                principalTable: "ProductVariant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
