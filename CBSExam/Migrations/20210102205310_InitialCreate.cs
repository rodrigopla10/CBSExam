using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CBSExam.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    messageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    createdDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    to = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.messageID);
                });

            migrationBuilder.CreateTable(
                name: "SentMessage",
                columns: table => new
                {
                    sentMessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateSent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    confirmationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    messageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentMessage", x => x.sentMessageID);
                    table.ForeignKey(
                        name: "FK_SentMessage_Message_messageID",
                        column: x => x.messageID,
                        principalTable: "Message",
                        principalColumn: "messageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SentMessage_messageID",
                table: "SentMessage",
                column: "messageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SentMessage");

            migrationBuilder.DropTable(
                name: "Message");
        }
    }
}
