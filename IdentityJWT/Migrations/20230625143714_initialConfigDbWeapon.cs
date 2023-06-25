using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityJWT.Migrations
{
    /// <inheritdoc />
    public partial class initialConfigDbWeapon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Heroes");

            migrationBuilder.CreateTable(
                name: "Weapon",
                schema: "Heroes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeaponName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WeaponDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapon", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weapon",
                schema: "Heroes");
        }
    }
}
