using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sharpcode.ServiceRadar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class remoteclient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessIssueOrganisationOrganisationId",
                table: "BusinessIssue",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RemoteClient",
                columns: table => new
                {
                    RemoteClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RemoteClientKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganisationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteClient", x => x.RemoteClientId);
                    table.ForeignKey(
                        name: "FK_RemoteClient_Organisation_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "OrganisationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessIssue_BusinessIssueOrganisationOrganisationId",
                table: "BusinessIssue",
                column: "BusinessIssueOrganisationOrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_RemoteClient_OrganisationId",
                table: "RemoteClient",
                column: "OrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessIssue_Organisation_BusinessIssueOrganisationOrganisationId",
                table: "BusinessIssue",
                column: "BusinessIssueOrganisationOrganisationId",
                principalTable: "Organisation",
                principalColumn: "OrganisationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessIssue_Organisation_BusinessIssueOrganisationOrganisationId",
                table: "BusinessIssue");

            migrationBuilder.DropTable(
                name: "RemoteClient");

            migrationBuilder.DropIndex(
                name: "IX_BusinessIssue_BusinessIssueOrganisationOrganisationId",
                table: "BusinessIssue");

            migrationBuilder.DropColumn(
                name: "BusinessIssueOrganisationOrganisationId",
                table: "BusinessIssue");
        }
    }
}
