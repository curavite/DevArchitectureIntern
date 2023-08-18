using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations.Ms
{
    /// <inheritdoc />
    public partial class AddfloorControllUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MachineType",
                table: "WashingControll_Floors",
                newName: "WashingMachine");

            migrationBuilder.AddColumn<string>(
                name: "DryingMachine",
                table: "WashingControll_Floors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SqueezMachine",
                table: "WashingControll_Floors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DryingMachine",
                table: "WashingControll_Floors");

            migrationBuilder.DropColumn(
                name: "SqueezMachine",
                table: "WashingControll_Floors");

            migrationBuilder.RenameColumn(
                name: "WashingMachine",
                table: "WashingControll_Floors",
                newName: "MachineType");
        }
    }
}
