using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMMSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyImgObj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageOriginalName",
                table: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageOriginalName",
                table: "Image",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
