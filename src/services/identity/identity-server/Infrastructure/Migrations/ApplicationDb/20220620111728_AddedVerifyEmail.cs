using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace identity_server.Infrastructure.Migrations.ApplicationDb
{
    public partial class AddedVerifyEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VerifyEmail",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidMinute = table.Column<int>(type: "int", nullable: false),
                    IsCodeValid = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerifyEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerifyEmail_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerifyEmail_UserId",
                schema: "identity",
                table: "VerifyEmail",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerifyEmail",
                schema: "identity");
        }
    }
}
