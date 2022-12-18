using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sharpcode.ServiceRadar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.ApplicationId);
                });

            migrationBuilder.CreateTable(
                name: "Issuer",
                columns: table => new
                {
                    IssuerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssuerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuerMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issuer", x => x.IssuerId);
                });

            migrationBuilder.CreateTable(
                name: "Organisation",
                columns: table => new
                {
                    OrganisationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Desription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisation", x => x.OrganisationId);
                });

            migrationBuilder.CreateTable(
                name: "BusinessIssue",
                columns: table => new
                {
                    BusinessIssueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssueType = table.Column<int>(type: "int", nullable: false),
                    BusinessIssuePriority = table.Column<int>(type: "int", nullable: false),
                    IssuedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ClosedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ImpactDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    IssuerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessIssue", x => x.BusinessIssueId);
                    table.ForeignKey(
                        name: "FK_BusinessIssue_Issuer_IssuerId",
                        column: x => x.IssuerId,
                        principalTable: "Issuer",
                        principalColumn: "IssuerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessIssue2Application",
                columns: table => new
                {
                    BusinessIssue2ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessIssueId = table.Column<int>(type: "int", nullable: false),
                    BusinessApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessIssue2Application", x => x.BusinessIssue2ApplicationId);
                    table.ForeignKey(
                        name: "FK_BusinessIssue2Application_Application_BusinessApplicationId",
                        column: x => x.BusinessApplicationId,
                        principalTable: "Application",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessIssue2Application_BusinessIssue_BusinessIssueId",
                        column: x => x.BusinessIssueId,
                        principalTable: "BusinessIssue",
                        principalColumn: "BusinessIssueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    BusinessIssueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Message_BusinessIssue_BusinessIssueId",
                        column: x => x.BusinessIssueId,
                        principalTable: "BusinessIssue",
                        principalColumn: "BusinessIssueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessIssue_IssuerId",
                table: "BusinessIssue",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessIssue2Application_BusinessApplicationId",
                table: "BusinessIssue2Application",
                column: "BusinessApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessIssue2Application_BusinessIssueId",
                table: "BusinessIssue2Application",
                column: "BusinessIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_BusinessIssueId",
                table: "Message",
                column: "BusinessIssueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessIssue2Application");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Organisation");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "BusinessIssue");

            migrationBuilder.DropTable(
                name: "Issuer");
        }
    }
}
