using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagment.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenameBuildingNumberColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_BuildingNumbern",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Address_BuildingNumbern",
                table: "Members");

            migrationBuilder.AddColumn<string>(
                name: "Address_BuildingNumber",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_BuildingNumber",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_BuildingNumber",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "Address_BuildingNumber",
                table: "Members");

            migrationBuilder.AddColumn<int>(
                name: "Address_BuildingNumbern",
                table: "Trainers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Address_BuildingNumbern",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
