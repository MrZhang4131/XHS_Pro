using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XHS_Pro.Migrations
{
    /// <inheritdoc />
    public partial class AddStartAndZan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Start",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<int>(type: "int", nullable: false),
                    noteid = table.Column<int>(type: "int", nullable: false),
                    enable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Start", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<int>(type: "int", nullable: false),
                    noteid = table.Column<int>(type: "int", nullable: false),
                    enable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zan", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Start");

            migrationBuilder.DropTable(
                name: "Zan");
        }
    }
}
