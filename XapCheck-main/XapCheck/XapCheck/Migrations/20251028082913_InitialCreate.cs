using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XapCheck.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserProfileId = table.Column<int>(nullable: true),
                    ExpiringSoonDays = table.Column<int>(nullable: false),
                    EnablePopupNotifications = table.Column<bool>(nullable: false),
                    Theme = table.Column<string>(nullable: true),
                    DefaultMinThreshold = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSettings_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Dosage = table.Column<string>(nullable: true),
                    Frequency = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    MinThreshold = table.Column<int>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    TreatmentStart = table.Column<DateTime>(nullable: true),
                    TreatmentEnd = table.Column<DateTime>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    UserProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActionType = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    PerformedBy = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    UserProfileId = table.Column<int>(nullable: true),
                    MedicineId = table.Column<int>(nullable: true),
                    QuantityDelta = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionLogs_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionLogs_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MedicineId = table.Column<int>(nullable: false),
                    UserProfileId = table.Column<int>(nullable: true),
                    RecommendedQuantity = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    IsResolved = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseList_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseList_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_MedicineId",
                table: "ActionLogs",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_UserProfileId",
                table: "ActionLogs",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSettings_UserProfileId",
                table: "AppSettings",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_UserProfileId",
                table: "Medicines",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseList_MedicineId",
                table: "PurchaseList",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseList_UserProfileId",
                table: "PurchaseList",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionLogs");

            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DropTable(
                name: "PurchaseList");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
