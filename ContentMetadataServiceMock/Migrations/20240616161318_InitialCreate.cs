using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentMetadataServiceMock.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LicenseRules",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MaxDevicesCount = table.Column<int>(type: "INTEGER", nullable: false),
                    LicenseType = table.Column<int>(type: "INTEGER", nullable: false),
                    LicenseStatus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseRules", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    ContentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Duration = table.Column<int>(type: "INTEGER", nullable: true),
                    LicenseRulesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_Contents_LicenseRules_LicenseRulesId",
                        column: x => x.LicenseRulesId,
                        principalTable: "LicenseRules",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentComments",
                columns: table => new
                {
                    ContentCommentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Body = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ContentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentComments", x => x.ContentCommentId);
                    table.ForeignKey(
                        name: "FK_ContentComments_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "ContentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentComments_ContentId",
                table: "ContentComments",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_LicenseRulesId",
                table: "Contents",
                column: "LicenseRulesId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentComments");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "LicenseRules");
        }
    }
}
