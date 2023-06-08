using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheCarHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCarModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model",
                table: "CarDetails");

            migrationBuilder.AddColumn<int>(
                name: "CarModelId",
                table: "CarDetails",
                type: "int",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateTable(
                name: "CarModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarDetails_CarModelId",
                table: "CarDetails",
                column: "CarModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarDetails_CarModel_CarModelId",
                table: "CarDetails",
                column: "CarModelId",
                principalTable: "CarModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarDetails_CarModel_CarModelId",
                table: "CarDetails");

            migrationBuilder.DropTable(
                name: "CarModel");

            migrationBuilder.DropIndex(
                name: "IX_CarDetails_CarModelId",
                table: "CarDetails");

            migrationBuilder.DropColumn(
                name: "CarModelId",
                table: "CarDetails");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "CarDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
