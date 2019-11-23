using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace angularclient.Migrations
{
    public partial class angularcrud_initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "TechRezUser",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechRezUser", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    PhotoUrl = table.Column<string>(nullable: true),
                    CategoryCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryCode",
                        column: x => x.CategoryCode,
                        principalTable: "Category",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    TechRezUserId = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    ProductCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Order_Product_ProductCode",
                        column: x => x.ProductCode,
                        principalTable: "Product",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_TechRezUser_TechRezUserId",
                        column: x => x.TechRezUserId,
                        principalTable: "TechRezUser",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Code", "Description" },
                values: new object[,]
                {
                    { "1", "Power Suply" },
                    { "2", "Motherboard" },
                    { "3", "Graphics Card" },
                    { "4", "RAM" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Code", "CategoryCode", "CategoryId", "Description", "PhotoUrl", "Price", "Quantity" },
                values: new object[,]
                {
                    { "36", null, 3, "GeForce GTX 1660 Super 6GB", "GFX/GeForce_GTX_1660_Super_6GB.jpg", 255.0, 47 },
                    { "37", null, 3, "AMD Radeon RX 590", "GFX/AMD_Radeon_RX_590.jpg", 430.0, 86 },
                    { "38", null, 3, "AMD Radeon RX 5700", "GFX/AMD_Radeon_RX_5700.jpg", 230.0, 38 },
                    { "39", null, 3, "Nvidia GeForce GTX 1660 Ti", "GFX/Nvidia_GeForce_GTX_1660_Ti.jpg", 245.0, 76 },
                    { "40", null, 4, "Corsair Vengeance LED", "RAM/Corsair_Vengeance_LED.jpg", 89.0, 65 },
                    { "41", null, 4, "G.Skill Trident Z RGB", "RAM/G_Skill_Trident_Z_RGB.jpg", 99.0, 65 },
                    { "42", null, 4, "Kingston HyperX Predator", "RAM/Kingston_HyperX_Predator.jpg", 199.0, 71 },
                    { "44", null, 4, "Corsair Dominator Platinum RGB", "RAM/Corsair_Dominator_Platinum_RGB.jpg", 175.0, 85 },
                    { "35", null, 3, "AMD Radeon RX 5700 XT", "GFX/AMD_Radeon_RX_5700_XT.jpg", 599.0, 81 },
                    { "45", null, 4, "G.Skill Trident Z RGB DC", "RAM/G_Skill_Trident_Z_RGB_DC.jpg", 146.0, 27 },
                    { "46", null, 4, "Adata Spectrix D80 ", "RAM/Adata_Spectrix_D80.jpg", 99.0, 19 },
                    { "47", null, 4, "HyperX Fury RGB", "RAM/HyperX_Fury_RGB.jpg", 187.0, 36 },
                    { "48", null, 4, "Corsair Vengeance LPX", "RAM/Corsair_Vengeance_LPX.jpg", 163.0, 85 },
                    { "49", null, 4, "G.Skill Mac RAM", "RAM/G_Skill_Mac_RAM.jpg", 165.0, 26 },
                    { "43", null, 4, "Kingston HyperX Fury ", "RAM/Kingston_HyperX_Fury .jpg", 140.0, 63 },
                    { "34", null, 3, "Nvidia GeForce RTX 2060 Super", "GFX/Nvidia_GeForce_RTX_2060_Super.jpg", 699.0, 73 },
                    { "32", null, 3, "Nvidia GeForce RTX 2080 Super", "GFX/Nvidia_GeForce_RTX_2080_Super.jpg", 860.0, 32 },
                    { "22", null, 2, "ASUS ROG Maximus XI Hero Wi-Fi", "MOB/ASUS_ROG_Maximus_XI_Hero_Wi_Fi.jpg", 178.0, 41 },
                    { "31", null, 3, "AMD Radeon RX 570 4GB", "GFX/AMD_Radeon_RX_570_4GB.jpg", 1072.0, 88 },
                    { "30", null, 3, "Nvidia GeForce RTX 2080 Ti", "GFX/Nvidia_GeForce_RTX_2080_Ti.jpg", 1088.0, 45 },
                    { "27", null, 2, "Gigabyte Z390 UD", "MOB/Gigabyte_Z390_UD.jpg", 245.0, 52 },
                    { "26", null, 2, "ASUS TUF H370-Pro Gaming Wi-Fi", "MOB/ASUS_TUF_H370_Pro_Gaming_Wi_Fi.jpg", 199.0, 16 },
                    { "25", null, 2, "Gigabyte X470 Aorus Gaming 5 Wi-Fi", "MOB/Gigabyte_X470_Aorus_Gaming_5_Wi_Fi.jpg", 175.0, 64 },
                    { "24", null, 2, "MSI MPG X570 Gaming Pro Carbon WiFi", "MOB/MSI_MPG_X570_Gaming_Pro_Carbon_WiFi.jpg", 189.0, 34 },
                    { "23", null, 2, "ASUS ROG Strix Z390-I Gaming", "MOB/ASUS_ROG_Strix_Z390_I_Gaming.jpg", 233.0, 85 },
                    { "33", null, 3, "Nvidia GeForce RTX 2070 Super", "GFX/Nvidia_GeForce_RTX_2070_Super.jpg", 890.0, 56 },
                    { "21", null, 2, "Gigabyte Z390 Aorus Ultra", "MOB/Gigabyte_Z390_Aorus_Ultra.jpg", 250.0, 63 },
                    { "16", null, 1, "Seasonic Prime 1000 Titanium", "PSU/Seasonic_Prime_1000_Titanium.jpg", 299.0, 86 },
                    { "15", null, 1, "NZXT E850", "PSU/NZXT_E850.jpg", 89.0, 86 },
                    { "14", null, 1, "Gamdias Astrape P1-750G", "PSU/Gamdias_Astrape_P1_750G.jpg", 227.0, 65 },
                    { "13", null, 1, "FSP Dagger 500W", "PSU/FSP_Dagger_500W.jpg", 184.0, 65 },
                    { "12", null, 1, "Cooler Master MasterWatt 750W", "PSU/Cooler_Master_MasterWatt_750W.jpg", 187.0, 87 },
                    { "11", null, 1, "Corsair RM850x", "PSU/Corsair_RM850x.jpg", 139.0, 52 }
                });

            migrationBuilder.InsertData(
                table: "TechRezUser",
                columns: new[] { "Code", "UserName" },
                values: new object[,]
                {
                    { "RandomDude_1", "RandomDude One" },
                    { "RandomDude_2", "RandomDude Two" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Code", "OrderDate", "ProductCode", "ProductId", "Quantity", "TechRezUserId" },
                values: new object[] { "1", new DateTime(2019, 11, 22, 8, 20, 7, 4, DateTimeKind.Local), null, 15, 1, "RandomDude_1" });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Code", "OrderDate", "ProductCode", "ProductId", "Quantity", "TechRezUserId" },
                values: new object[] { "2", new DateTime(2019, 11, 18, 4, 24, 11, 6, DateTimeKind.Local), null, 24, 2, "RandomDude_2" });

            migrationBuilder.CreateIndex(
                name: "IX_Order_ProductCode",
                table: "Order",
                column: "ProductCode");

            migrationBuilder.CreateIndex(
                name: "IX_Order_TechRezUserId",
                table: "Order",
                column: "TechRezUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryCode",
                table: "Product",
                column: "CategoryCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "TechRezUser");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
