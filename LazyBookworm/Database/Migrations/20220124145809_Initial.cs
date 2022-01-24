using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace LazyBookworm.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_accounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LoginDetails_Username = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginDetails_Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastLogin = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2022, 1, 24, 14, 58, 9, 502, DateTimeKind.Utc).AddTicks(3520)),
                    AccountCreation = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2022, 1, 24, 14, 58, 9, 502, DateTimeKind.Utc).AddTicks(3750)),
                    PermissionLevel = table.Column<int>(type: "int", nullable: false),
                    IsSuspended = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PersonDetails_Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonDetails_Forename = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonDetails_Gender = table.Column<int>(type: "int", nullable: true),
                    PersonDetails_BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PersonDetails_Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonDetails_Country = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonDetails_MailAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonDetails_Phone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PersonDetails_Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_accounts", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_user_accounts_ID",
                table: "user_accounts",
                column: "ID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_accounts");
        }
    }
}