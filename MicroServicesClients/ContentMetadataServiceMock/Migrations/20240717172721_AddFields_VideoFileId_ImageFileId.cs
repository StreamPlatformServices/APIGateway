using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentMetadataServiceMock.Migrations
{
    /// <inheritdoc />
    public partial class AddFields_VideoFileId_ImageFileId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentStatus",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ImageStatus",
                table: "Contents");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageFileId",
                table: "Contents",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VideoFileId",
                table: "Contents",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "VideoFileId",
                table: "Contents");

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
    }
}
