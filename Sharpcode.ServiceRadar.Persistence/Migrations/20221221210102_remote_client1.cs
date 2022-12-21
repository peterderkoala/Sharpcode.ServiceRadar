using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sharpcode.ServiceRadar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class remoteclient1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Organisation",
                columns: new[] { "OrganisationId", "Desription", "Title" },
                values: new object[] { 1, "Default organisation after initial migration", "Default" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Organisation",
                keyColumn: "OrganisationId",
                keyValue: 1);
        }
    }
}
