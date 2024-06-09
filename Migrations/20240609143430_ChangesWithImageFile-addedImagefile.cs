using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace autoCadApiDevelopment.Migrations
{
    /// <inheritdoc />
    public partial class ChangesWithImageFileaddedImagefile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AutoCADFileId",
                table: "Pins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Urn",
                table: "ImageFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AutoCADFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Urn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoCADFile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pins_AutoCADFileId",
                table: "Pins",
                column: "AutoCADFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pins_AutoCADFile_AutoCADFileId",
                table: "Pins",
                column: "AutoCADFileId",
                principalTable: "AutoCADFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pins_AutoCADFile_AutoCADFileId",
                table: "Pins");

            migrationBuilder.DropTable(
                name: "AutoCADFile");

            migrationBuilder.DropIndex(
                name: "IX_Pins_AutoCADFileId",
                table: "Pins");

            migrationBuilder.DropColumn(
                name: "AutoCADFileId",
                table: "Pins");

            migrationBuilder.DropColumn(
                name: "Urn",
                table: "ImageFiles");
        }
    }
}
