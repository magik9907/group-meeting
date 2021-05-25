using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupMeeting.Migrations
{
    public partial class CityGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GroupCity",
                columns: table => new
                {
                    GroupID = table.Column<int>(nullable: false),
                    CityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCity", x => new { x.GroupID, x.CityID });
                    table.ForeignKey(
                        name: "FK_GroupCity_Groups_CityID",
                        column: x => x.CityID,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupCity_Cities_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupCity_CityID",
                table: "GroupCity",
                column: "CityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupCity");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
