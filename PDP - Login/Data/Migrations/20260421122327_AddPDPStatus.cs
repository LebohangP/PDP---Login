using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PDP___Login.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPDPStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PDP_AspNetUsers_UserId",
                table: "PDP");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PDP",
                table: "PDP");

            migrationBuilder.RenameTable(
                name: "PDP",
                newName: "PDPs");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PDPs",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "GetDate",
                table: "PDPs",
                newName: "DateSubmitted");

            migrationBuilder.RenameIndex(
                name: "IX_PDP_UserId",
                table: "PDPs",
                newName: "IX_PDPs_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PDPs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "PDPs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PDPs",
                table: "PDPs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PDPFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PDPFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PDPFiles_PDPs_Id",
                        column: x => x.Id,
                        principalTable: "PDPs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PDPFiles_Id",
                table: "PDPFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PDPs_AspNetUsers_UserId",
                table: "PDPs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PDPs_AspNetUsers_UserId",
                table: "PDPs");

            migrationBuilder.DropTable(
                name: "PDPFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PDPs",
                table: "PDPs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PDPs");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "PDPs");

            migrationBuilder.RenameTable(
                name: "PDPs",
                newName: "PDP");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "PDP",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DateSubmitted",
                table: "PDP",
                newName: "GetDate");

            migrationBuilder.RenameIndex(
                name: "IX_PDPs_UserId",
                table: "PDP",
                newName: "IX_PDP_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PDP",
                table: "PDP",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PDP_AspNetUsers_UserId",
                table: "PDP",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
