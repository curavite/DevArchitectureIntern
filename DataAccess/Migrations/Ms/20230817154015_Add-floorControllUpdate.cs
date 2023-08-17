using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations.Ms
{
    /// <inheritdoc />
    public partial class AddfloorControllUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorName",
                table: "WashingControll_FloorControlls");

            migrationBuilder.AlterColumn<int>(
                name: "FaultyProduct",
                table: "WashingControll_FloorControlls",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FaultyProduct",
                table: "WashingControll_FloorControlls",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ErrorName",
                table: "WashingControll_FloorControlls",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
