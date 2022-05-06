using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntiPsychRRMVC.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drug",
                columns: table => new
                {
                    DrugId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrugMaxDoseID = table.Column<int>(type: "int", nullable: false),
                    DrugRouteID = table.Column<int>(type: "int", nullable: false),
                    DrugFrequencyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drug", x => x.DrugId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drug");
        }
    }
}
