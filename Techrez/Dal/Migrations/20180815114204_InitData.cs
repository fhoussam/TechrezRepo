using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal.Migrations
{
    public partial class InitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description" },
                values: new object[] { 1, "Power supply unit" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description" },
                values: new object[] { 2, "CPU" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description" },
                values: new object[] { 3, "Graphics card" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryID", "Description", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "Earthwatts 500w", 56f, 23 },
                    { 2, 1, "EVGA 850w", 80f, 14 },
                    { 3, 1, "Cooler master 750w", 69f, 8 },
                    { 4, 1, "Corsair 850W", 250f, 16 },
                    { 5, 1, "BeQuiet! 450W", 250f, 16 },
                    { 6, 1, "Enermax 600W", 250f, 16 },
                    { 7, 2, "Intel i5 3750k", 250f, 31 },
                    { 8, 2, "Intel i7 4770k", 320f, 15 },
                    { 9, 2, "Intel i7 3770k", 299f, 6 },
                    { 10, 2, "Intel Quad Q6600", 80f, 8 },
                    { 11, 2, "Intel i5 3550K", 130f, 7 },
                    { 12, 3, "Nvidia 980 GTX", 280f, 4 },
                    { 13, 3, "MSI 1080 ti Gaming", 550f, 20 },
                    { 14, 3, "MSI 970 GTX Gaming", 199f, 78 },
                    { 15, 3, "Asus 1080 GTX Strix OC", 449f, 26 },
                    { 16, 3, "Gigabyte 1060 GTX Windforce", 220f, 13 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
