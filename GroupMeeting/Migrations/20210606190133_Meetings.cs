using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroupMeeting.Migrations
{
    public partial class Meetings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group_id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Start_Date = table.Column<DateTime>(nullable: false),
                    Start_Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Meetings_Groups_Group_id",
                        column: x => x.Group_id,
                        principalTable: "Groups",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                }) ;
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meetings");
        }
    }
}