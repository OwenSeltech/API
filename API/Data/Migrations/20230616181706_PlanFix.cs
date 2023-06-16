using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class PlanFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "SponsorshipPlans");

            migrationBuilder.AddColumn<string>(
                name: "SponsorshipFrequency",
                table: "SponsorshipPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SponsorshipFrequency",
                table: "SponsorshipPlans");

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "SponsorshipPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
