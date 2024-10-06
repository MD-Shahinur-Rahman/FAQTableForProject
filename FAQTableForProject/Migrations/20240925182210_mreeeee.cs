using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAQTableForProject.Migrations
{
    /// <inheritdoc />
    public partial class mreeeee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoName",
                table: "Partners",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoName",
                table: "Partners");
        }
    }
}
