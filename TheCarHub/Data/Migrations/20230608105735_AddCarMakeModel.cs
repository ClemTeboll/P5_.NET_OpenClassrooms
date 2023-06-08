using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheCarHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCarMakeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarDetails_CarModel_CarModelId",
                table: "CarDetails");

            migrationBuilder.DropColumn(
                name: "Make",
                table: "CarDetails");

            migrationBuilder.AlterColumn<int>(
                name: "CarModelId",
                table: "CarDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CarMakesId",
                table: "CarDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarMakes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarMakes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarDetails_CarMakesId",
                table: "CarDetails",
                column: "CarMakesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarDetails_CarMakes_CarMakesId",
                table: "CarDetails",
                column: "CarMakesId",
                principalTable: "CarMakes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarDetails_CarModel_CarModelId",
                table: "CarDetails",
                column: "CarModelId",
                principalTable: "CarModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarDetails_CarMakes_CarMakesId",
                table: "CarDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CarDetails_CarModel_CarModelId",
                table: "CarDetails");

            migrationBuilder.DropTable(
                name: "CarMakes");

            migrationBuilder.DropIndex(
                name: "IX_CarDetails_CarMakesId",
                table: "CarDetails");

            migrationBuilder.DropColumn(
                name: "CarMakesId",
                table: "CarDetails");

            migrationBuilder.AlterColumn<int>(
                name: "CarModelId",
                table: "CarDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "CarDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_CarDetails_CarModel_CarModelId",
                table: "CarDetails",
                column: "CarModelId",
                principalTable: "CarModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
