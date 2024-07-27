using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentMetadataServiceMock.Migrations
{
    /// <inheritdoc />
    public partial class AddColumn_ContentMetadataTable_OwnerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Contents");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Contents",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Contents");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Contents",
                type: "TEXT",
                nullable: true);
        }
    }
}
