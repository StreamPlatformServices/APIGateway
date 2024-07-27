using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentMetadataServiceMock.Migrations
{
    /// <inheritdoc />
    public partial class AddColumns_ImageStateAndVideoState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentStatus",
                table: "Contents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageStatus",
                table: "Contents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentStatus",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ImageStatus",
                table: "Contents");
        }
    }
}
