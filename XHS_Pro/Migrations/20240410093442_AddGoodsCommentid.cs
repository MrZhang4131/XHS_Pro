using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XHS_Pro.Migrations
{
    /// <inheritdoc />
    public partial class AddGoodsCommentid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "goodsid",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "goodsid",
                table: "Comment");
        }
    }
}
