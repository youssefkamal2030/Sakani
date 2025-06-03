using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sakani.DA.Migrations
{
    /// <inheritdoc />
    public partial class religon_stduent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Religon",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Religon",
                table: "Students");
        }
    }
}
