using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sakani.DA.Migrations
{
    /// <inheritdoc />
    public partial class student_phone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phonenumber",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phonenumber",
                table: "Students");
        }
    }
}
