using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace autoCadApiDevelopment.Migrations
{
    /// <inheritdoc />
    public partial class ChangesWithImageFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pins_AutoCADFiles_AutoCADFileId",
                table: "Pins");

            migrationBuilder.DropTable(
                name: "AutoCADFiles");

            migrationBuilder.RenameColumn(
                name: "AutoCADFileId",
                table: "Pins",
                newName: "ImageFileId");

            migrationBuilder.RenameIndex(
                name: "IX_Pins_AutoCADFileId",
                table: "Pins",
                newName: "IX_Pins_ImageFileId");

            migrationBuilder.CreateTable(
                name: "ImageFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageFiles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Pins_ImageFiles_ImageFileId",
                table: "Pins",
                column: "ImageFileId",
                principalTable: "ImageFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pins_ImageFiles_ImageFileId",
                table: "Pins");

            migrationBuilder.DropTable(
                name: "ImageFiles");

            migrationBuilder.RenameColumn(
                name: "ImageFileId",
                table: "Pins",
                newName: "AutoCADFileId");

            migrationBuilder.RenameIndex(
                name: "IX_Pins_ImageFileId",
                table: "Pins",
                newName: "IX_Pins_AutoCADFileId");

            migrationBuilder.CreateTable(
                name: "AutoCADFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Urn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoCADFiles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Pins_AutoCADFiles_AutoCADFileId",
                table: "Pins",
                column: "AutoCADFileId",
                principalTable: "AutoCADFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
