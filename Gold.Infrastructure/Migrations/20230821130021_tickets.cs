using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gold.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketMessages",
                columns: table => new
                {
                    MessageId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MessageSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketMessages", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusEnum = table.Column<int>(type: "int", nullable: false),
                    TicketId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Tickets_TicketId1",
                        column: x => x.TicketId1,
                        principalTable: "Tickets",
                        principalColumn: "TicketId");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PayBillId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssetCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPayed = table.Column<bool>(type: "bit", nullable: false),
                    AssetId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemCashToGolds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EarnedGold = table.Column<double>(type: "float", nullable: false),
                    SpentCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeftCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delay = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    ToAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemCashToGolds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemCashToGolds_AspNetUsers_ToAppUserId",
                        column: x => x.ToAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SystemGoldToCashs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalGold = table.Column<double>(type: "float", nullable: false),
                    TotalCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoldGold = table.Column<double>(type: "float", nullable: false),
                    LeftGold = table.Column<double>(type: "float", nullable: false),
                    EarnedCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeftCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delay = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    ToAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemGoldToCashs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemGoldToCashs_AspNetUsers_ToAppUserId",
                        column: x => x.ToAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserAssets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gold = table.Column<double>(type: "float", nullable: false),
                    Cash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAssets_AspNetUsers_ToAppUserId",
                        column: x => x.ToAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserCashGolds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeftCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpentCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EarnedGold = table.Column<double>(type: "float", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delay = table.Column<long>(type: "bigint", nullable: false),
                    IsPayed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    ToAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCashGolds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCashGolds_AspNetUsers_ToAppUserId",
                        column: x => x.ToAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserGoldCashs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalGold = table.Column<double>(type: "float", nullable: false),
                    SpentGold = table.Column<double>(type: "float", nullable: false),
                    LeftGold = table.Column<double>(type: "float", nullable: false),
                    EarnedCash = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Delay = table.Column<long>(type: "bigint", nullable: false),
                    IsPayed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    ToAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGoldCashs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGoldCashs_AspNetUsers_ToAppUserId",
                        column: x => x.ToAppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GoldPrices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoldPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoldPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSystemCashToGoldBills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CashAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GoldAmount = table.Column<double>(type: "float", nullable: false),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToUserCashToGoldId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToSystemGoldToCashId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSystemCashToGoldBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSystemCashToGoldBills_SystemGoldToCashs_ToSystemGoldToCashId",
                        column: x => x.ToSystemGoldToCashId,
                        principalTable: "SystemGoldToCashs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSystemCashToGoldBills_UserCashGolds_ToUserCashToGoldId",
                        column: x => x.ToUserCashToGoldId,
                        principalTable: "UserCashGolds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSystemGoldToCashBills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CashAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GoldAmount = table.Column<double>(type: "float", nullable: false),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToUserGoldToCashId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToSystemCashToGoldId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSystemGoldToCashBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSystemGoldToCashBills_SystemCashToGolds_ToSystemCashToGoldId",
                        column: x => x.ToSystemCashToGoldId,
                        principalTable: "SystemCashToGolds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSystemGoldToCashBills_UserGoldCashs_ToUserGoldToCashId",
                        column: x => x.ToUserGoldToCashId,
                        principalTable: "UserGoldCashs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { "ad6151ed16984999b3ff121c5c0bfd96", "1", null, "MainAdmin", "MainAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatDateTime", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8c72dff883b3403e81dc7bf88ce26b8a", 0, "daa973fe-0f9f-4173-bfdf-3a5731b047f6", new DateTime(2023, 8, 21, 16, 30, 20, 807, DateTimeKind.Local).AddTicks(2848), null, false, "Mahdi", "Montazeri", false, null, null, null, "AQAAAAIAAYagAAAAEELD1V7vWrfPtItdV1NtSA39X5X9guVfPmclYN5hDOo1yuPIXikB4eOT7UrunSRbHw==", "09125850371", false, "1", false, "MainAdmin" });

            migrationBuilder.InsertData(
                table: "GoldPrices",
                columns: new[] { "Id", "DateTime", "Price", "ProductId" },
                values: new object[] { "106f2edd-0745-4eb6-9a28-2fcb71db6060", new DateTime(2023, 8, 21, 16, 30, 20, 812, DateTimeKind.Local).AddTicks(430), 105000.0, null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "UnitName" },
                values: new object[] { "3aa1fedb-e9b5-4057-b002-fba02046a141", "Gold", "گرم" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ad6151ed16984999b3ff121c5c0bfd96", "8c72dff883b3403e81dc7bf88ce26b8a" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhoneNumber",
                table: "AspNetUsers",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GoldPrices_ProductId",
                table: "GoldPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemCashToGolds_ToAppUserId",
                table: "SystemCashToGolds",
                column: "ToAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemGoldToCashs_ToAppUserId",
                table: "SystemGoldToCashs",
                column: "ToAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketId1",
                table: "Tickets",
                column: "TicketId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssets_ToAppUserId",
                table: "UserAssets",
                column: "ToAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCashGolds_ToAppUserId",
                table: "UserCashGolds",
                column: "ToAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGoldCashs_ToAppUserId",
                table: "UserGoldCashs",
                column: "ToAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSystemCashToGoldBills_ToSystemGoldToCashId",
                table: "UserSystemCashToGoldBills",
                column: "ToSystemGoldToCashId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSystemCashToGoldBills_ToUserCashToGoldId",
                table: "UserSystemCashToGoldBills",
                column: "ToUserCashToGoldId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSystemGoldToCashBills_ToSystemCashToGoldId",
                table: "UserSystemGoldToCashBills",
                column: "ToSystemCashToGoldId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSystemGoldToCashBills_ToUserGoldToCashId",
                table: "UserSystemGoldToCashBills",
                column: "ToUserGoldToCashId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GoldPrices");

            migrationBuilder.DropTable(
                name: "TicketMessages");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UserAssets");

            migrationBuilder.DropTable(
                name: "UserSystemCashToGoldBills");

            migrationBuilder.DropTable(
                name: "UserSystemGoldToCashBills");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SystemGoldToCashs");

            migrationBuilder.DropTable(
                name: "UserCashGolds");

            migrationBuilder.DropTable(
                name: "SystemCashToGolds");

            migrationBuilder.DropTable(
                name: "UserGoldCashs");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
