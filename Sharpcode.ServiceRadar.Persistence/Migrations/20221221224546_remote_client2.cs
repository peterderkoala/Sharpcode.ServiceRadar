using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sharpcode.ServiceRadar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class remoteclient2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessIssue_Organisation_BusinessIssueOrganisationOrganisationId",
                table: "BusinessIssue");

            migrationBuilder.RenameColumn(
                name: "BusinessIssueOrganisationOrganisationId",
                table: "BusinessIssue",
                newName: "OrganisationId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessIssue_BusinessIssueOrganisationOrganisationId",
                table: "BusinessIssue",
                newName: "IX_BusinessIssue_OrganisationId");

            migrationBuilder.InsertData(
                table: "Application",
                columns: new[] { "ApplicationId", "CreatedAt", "DeletedAt", "Description", "Title", "UpdatedAt", "Version" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Demo Application after initial migration", "Demo Application", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "0.0.1" });

            migrationBuilder.InsertData(
                table: "Issuer",
                columns: new[] { "IssuerId", "CreatedAt", "DeletedAt", "IssuerMail", "IssuerName" },
                values: new object[] { 1, new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), null, "demo@serviceradar.net", "Demo Issuer" });

            migrationBuilder.InsertData(
                table: "BusinessIssue",
                columns: new[] { "BusinessIssueId", "Body", "BusinessIssuePriority", "ClosedAt", "ImpactDuration", "IssueType", "IssuedAt", "IssuerId", "OrganisationId", "Title" },
                values: new object[] { 1, "This is a demo issue after initial migration", 2, null, new TimeSpan(0, 1, 1, 1, 0), 0, new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), 1, 1, "Demo issue" });

            migrationBuilder.InsertData(
                table: "BusinessIssue2Application",
                columns: new[] { "BusinessIssue2ApplicationId", "BusinessApplicationId", "BusinessIssueId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Message",
                columns: new[] { "MessageId", "BusinessIssueId", "CreatedAt", "MessageBody", "Title" },
                values: new object[] { 1, 1, new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "First demo message.", "Demo Message 1" });

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessIssue_Organisation_OrganisationId",
                table: "BusinessIssue",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "OrganisationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessIssue_Organisation_OrganisationId",
                table: "BusinessIssue");

            migrationBuilder.DeleteData(
                table: "BusinessIssue2Application",
                keyColumn: "BusinessIssue2ApplicationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Message",
                keyColumn: "MessageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Application",
                keyColumn: "ApplicationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BusinessIssue",
                keyColumn: "BusinessIssueId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Issuer",
                keyColumn: "IssuerId",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "OrganisationId",
                table: "BusinessIssue",
                newName: "BusinessIssueOrganisationOrganisationId");

            migrationBuilder.RenameIndex(
                name: "IX_BusinessIssue_OrganisationId",
                table: "BusinessIssue",
                newName: "IX_BusinessIssue_BusinessIssueOrganisationOrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessIssue_Organisation_BusinessIssueOrganisationOrganisationId",
                table: "BusinessIssue",
                column: "BusinessIssueOrganisationOrganisationId",
                principalTable: "Organisation",
                principalColumn: "OrganisationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
