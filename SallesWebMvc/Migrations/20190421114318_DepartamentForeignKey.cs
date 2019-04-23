using Microsoft.EntityFrameworkCore.Migrations;

namespace SallesWebMvc.Migrations
{
    public partial class DepartamentForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seller_Departament_DepartamentID",
                table: "Seller");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentID",
                table: "Seller",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_Departament_DepartamentID",
                table: "Seller",
                column: "DepartamentID",
                principalTable: "Departament",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seller_Departament_DepartamentID",
                table: "Seller");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentID",
                table: "Seller",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_Departament_DepartamentID",
                table: "Seller",
                column: "DepartamentID",
                principalTable: "Departament",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
