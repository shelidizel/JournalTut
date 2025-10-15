using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JournalAPI.Migrations
{
    /// <inheritdoc />
    public partial class somechangess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "JournalBS",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JournalBS_UnitId",
                table: "JournalBS",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalBS_Units_UnitId",
                table: "JournalBS",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalBS_Units_UnitId",
                table: "JournalBS");

            migrationBuilder.DropIndex(
                name: "IX_JournalBS_UnitId",
                table: "JournalBS");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "JournalBS");
        }
    }
}
