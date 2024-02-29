using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerTrack.Migrations
{
    /// <inheritdoc />
    public partial class AddJobPostingSourceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Job",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "Job");
        }
    }
}
