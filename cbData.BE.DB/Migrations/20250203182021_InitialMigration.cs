﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cbData.BE.DB.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Products");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UpdateUtcDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Products",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Products",
                table: "Products",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Product 1" },
                    { 2, null, "Product 2" },
                    { 3, null, "Product 3" },
                    { 4, null, "Product 4" },
                    { 5, null, "Product 5" },
                    { 6, null, "Product 6" },
                    { 7, null, "Product 7" },
                    { 8, null, "Product 8" },
                    { 9, null, "Product 9" },
                    { 10, null, "Product 10" },
                    { 11, null, "Product 11" },
                    { 12, null, "Product 12" },
                    { 13, null, "Product 13" },
                    { 14, null, "Product 14" },
                    { 15, null, "Product 15" },
                    { 16, null, "Product 16" },
                    { 17, null, "Product 17" },
                    { 18, null, "Product 18" },
                    { 19, null, "Product 19" },
                    { 20, null, "Product 20" },
                    { 21, null, "Product 21" },
                    { 22, null, "Product 22" },
                    { 23, null, "Product 23" },
                    { 24, null, "Product 24" },
                    { 25, null, "Product 25" },
                    { 26, null, "Product 26" },
                    { 27, null, "Product 27" },
                    { 28, null, "Product 28" },
                    { 29, null, "Product 29" },
                    { 30, null, "Product 30" },
                    { 31, null, "Product 31" },
                    { 32, null, "Product 32" },
                    { 33, null, "Product 33" },
                    { 34, null, "Product 34" },
                    { 35, null, "Product 35" },
                    { 36, null, "Product 36" },
                    { 37, null, "Product 37" },
                    { 38, null, "Product 38" },
                    { 39, null, "Product 39" },
                    { 40, null, "Product 40" },
                    { 41, null, "Product 41" },
                    { 42, null, "Product 42" },
                    { 43, null, "Product 43" },
                    { 44, null, "Product 44" },
                    { 45, null, "Product 45" },
                    { 46, null, "Product 46" },
                    { 47, null, "Product 47" },
                    { 48, null, "Product 48" },
                    { 49, null, "Product 49" },
                    { 50, null, "Product 50" },
                    { 51, null, "Product 51" },
                    { 52, null, "Product 52" },
                    { 53, null, "Product 53" },
                    { 54, null, "Product 54" },
                    { 55, null, "Product 55" },
                    { 56, null, "Product 56" },
                    { 57, null, "Product 57" },
                    { 58, null, "Product 58" },
                    { 59, null, "Product 59" },
                    { 60, null, "Product 60" },
                    { 61, null, "Product 61" },
                    { 62, null, "Product 62" },
                    { 63, null, "Product 63" },
                    { 64, null, "Product 64" },
                    { 65, null, "Product 65" },
                    { 66, null, "Product 66" },
                    { 67, null, "Product 67" },
                    { 68, null, "Product 68" },
                    { 69, null, "Product 69" },
                    { 70, null, "Product 70" },
                    { 71, null, "Product 71" },
                    { 72, null, "Product 72" },
                    { 73, null, "Product 73" },
                    { 74, null, "Product 74" },
                    { 75, null, "Product 75" },
                    { 76, null, "Product 76" },
                    { 77, null, "Product 77" },
                    { 78, null, "Product 78" },
                    { 79, null, "Product 79" },
                    { 80, null, "Product 80" },
                    { 81, null, "Product 81" },
                    { 82, null, "Product 82" },
                    { 83, null, "Product 83" },
                    { 84, null, "Product 84" },
                    { 85, null, "Product 85" },
                    { 86, null, "Product 86" },
                    { 87, null, "Product 87" },
                    { 88, null, "Product 88" },
                    { 89, null, "Product 89" },
                    { 90, null, "Product 90" },
                    { 91, null, "Product 91" },
                    { 92, null, "Product 92" },
                    { 93, null, "Product 93" },
                    { 94, null, "Product 94" },
                    { 95, null, "Product 95" },
                    { 96, null, "Product 96" },
                    { 97, null, "Product 97" },
                    { 98, null, "Product 98" },
                    { 99, null, "Product 99" }
                });

            migrationBuilder.InsertData(
                schema: "Products",
                table: "Orders",
                columns: new[] { "Id", "ProductId", "Quantity", "UpdateUtcDateTime" },
                values: new object[,]
                {
                    { 1, 70, 198, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2532) },
                    { 2, 91, 115, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2567) },
                    { 3, 78, 197, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2581) },
                    { 4, 87, 99, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2598) },
                    { 5, 88, 40, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2611) },
                    { 6, 86, 58, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2628) },
                    { 7, 79, 180, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2641) },
                    { 8, 97, 49, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2657) },
                    { 9, 85, 109, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2670) },
                    { 10, 94, 115, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2686) },
                    { 11, 39, 148, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2699) },
                    { 12, 63, 43, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2714) },
                    { 13, 28, 165, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2728) },
                    { 14, 26, 141, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2743) },
                    { 15, 75, 128, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2756) },
                    { 16, 85, 177, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2771) },
                    { 17, 38, 113, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2819) },
                    { 18, 17, 91, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2840) },
                    { 19, 26, 15, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2853) },
                    { 20, 15, 192, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2869) },
                    { 21, 46, 147, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2883) },
                    { 22, 58, 177, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2898) },
                    { 23, 68, 149, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2912) },
                    { 24, 19, 105, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2928) },
                    { 25, 15, 191, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2941) },
                    { 26, 96, 123, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2957) },
                    { 27, 80, 57, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2971) },
                    { 28, 78, 130, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(2986) },
                    { 29, 83, 116, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3000) },
                    { 30, 3, 129, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3015) },
                    { 31, 83, 179, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3028) },
                    { 32, 47, 22, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3043) },
                    { 33, 33, 154, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3057) },
                    { 34, 60, 112, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3074) },
                    { 35, 74, 168, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3122) },
                    { 36, 38, 120, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3138) },
                    { 37, 77, 15, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3152) },
                    { 38, 78, 78, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3168) },
                    { 39, 43, 152, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3181) },
                    { 40, 42, 136, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3198) },
                    { 41, 14, 122, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3211) },
                    { 42, 4, 96, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3226) },
                    { 43, 16, 65, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3240) },
                    { 44, 53, 17, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3255) },
                    { 45, 96, 8, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3268) },
                    { 46, 54, 93, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3284) },
                    { 47, 20, 111, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3298) },
                    { 48, 63, 80, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3313) },
                    { 49, 48, 163, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3327) },
                    { 50, 69, 76, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3342) },
                    { 51, 6, 46, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3355) },
                    { 52, 18, 60, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3370) },
                    { 53, 18, 168, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3383) },
                    { 54, 57, 104, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3399) },
                    { 55, 36, 65, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3459) },
                    { 56, 83, 117, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3475) },
                    { 57, 12, 153, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3488) },
                    { 58, 92, 118, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3503) },
                    { 59, 53, 78, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3517) },
                    { 60, 77, 191, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3533) },
                    { 61, 28, 49, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3546) },
                    { 62, 90, 94, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3562) },
                    { 63, 26, 146, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3575) },
                    { 64, 98, 182, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3591) },
                    { 65, 87, 189, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3605) },
                    { 66, 66, 128, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3623) },
                    { 67, 88, 132, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3636) },
                    { 68, 45, 45, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3652) },
                    { 69, 68, 39, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3665) },
                    { 70, 4, 52, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3680) },
                    { 71, 19, 13, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3693) },
                    { 72, 89, 146, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3719) },
                    { 73, 75, 17, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3733) },
                    { 74, 24, 3, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3748) },
                    { 75, 1, 161, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3761) },
                    { 76, 31, 87, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3777) },
                    { 77, 87, 108, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3790) },
                    { 78, 45, 156, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3806) },
                    { 79, 18, 26, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3819) },
                    { 80, 93, 136, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3834) },
                    { 81, 45, 150, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3847) },
                    { 82, 97, 151, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3863) },
                    { 83, 65, 42, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3876) },
                    { 84, 15, 184, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3891) },
                    { 85, 92, 125, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3904) },
                    { 86, 74, 99, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3920) },
                    { 87, 9, 145, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3933) },
                    { 88, 90, 30, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3949) },
                    { 89, 57, 102, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3962) },
                    { 90, 33, 197, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3979) },
                    { 91, 74, 147, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(3992) },
                    { 92, 25, 115, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4017) },
                    { 93, 34, 12, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4029) },
                    { 94, 51, 157, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4044) },
                    { 95, 64, 182, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4058) },
                    { 96, 63, 107, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4074) },
                    { 97, 93, 163, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4087) },
                    { 98, 66, 115, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4102) },
                    { 99, 78, 154, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4116) },
                    { 100, 82, 40, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4131) },
                    { 101, 3, 176, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4145) },
                    { 102, 22, 122, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4161) },
                    { 103, 30, 34, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4174) },
                    { 104, 56, 188, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4189) },
                    { 105, 59, 157, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4202) },
                    { 106, 27, 188, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4218) },
                    { 107, 42, 88, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4232) },
                    { 108, 88, 30, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4248) },
                    { 109, 41, 47, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4262) },
                    { 110, 93, 62, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4277) },
                    { 111, 83, 135, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4302) },
                    { 112, 77, 122, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4318) },
                    { 113, 55, 1, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4331) },
                    { 114, 81, 3, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4347) },
                    { 115, 98, 100, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4360) },
                    { 116, 37, 92, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4375) },
                    { 117, 56, 53, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4388) },
                    { 118, 38, 180, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4404) },
                    { 119, 8, 5, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4418) },
                    { 120, 34, 124, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4433) },
                    { 121, 42, 52, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4447) },
                    { 122, 20, 194, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4462) },
                    { 123, 29, 102, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4476) },
                    { 124, 44, 140, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4491) },
                    { 125, 65, 26, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4504) },
                    { 126, 51, 77, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4520) },
                    { 127, 17, 108, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4534) },
                    { 128, 46, 38, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4549) },
                    { 129, 78, 163, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4563) },
                    { 130, 51, 136, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4588) },
                    { 131, 70, 179, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4602) },
                    { 132, 75, 175, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4618) },
                    { 133, 81, 107, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4632) },
                    { 134, 91, 26, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4648) },
                    { 135, 87, 53, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4661) },
                    { 136, 59, 44, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4676) },
                    { 137, 73, 36, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4690) },
                    { 138, 33, 20, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4705) },
                    { 139, 27, 145, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4719) },
                    { 140, 80, 90, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4734) },
                    { 141, 72, 99, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4748) },
                    { 142, 62, 121, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4763) },
                    { 143, 5, 66, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4776) },
                    { 144, 39, 122, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4792) },
                    { 145, 29, 146, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4805) },
                    { 146, 70, 4, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4847) },
                    { 147, 23, 125, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4861) },
                    { 148, 28, 88, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4876) },
                    { 149, 8, 188, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4890) },
                    { 150, 22, 85, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4904) },
                    { 151, 4, 59, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4918) },
                    { 152, 48, 1, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4933) },
                    { 153, 4, 170, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4947) },
                    { 154, 8, 78, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4962) },
                    { 155, 62, 105, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4975) },
                    { 156, 76, 195, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(4991) },
                    { 157, 54, 154, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5049) },
                    { 158, 44, 147, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5065) },
                    { 159, 30, 34, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5080) },
                    { 160, 93, 103, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5095) },
                    { 161, 96, 49, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5109) },
                    { 162, 98, 94, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5125) },
                    { 163, 75, 187, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5138) },
                    { 164, 52, 52, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5154) },
                    { 165, 4, 62, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5168) },
                    { 166, 46, 62, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5184) },
                    { 167, 60, 126, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5198) },
                    { 168, 30, 91, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5213) },
                    { 169, 51, 180, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5228) },
                    { 170, 71, 175, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5243) },
                    { 171, 54, 88, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5257) },
                    { 172, 6, 40, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5273) },
                    { 173, 82, 81, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5287) },
                    { 174, 72, 111, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5303) },
                    { 175, 75, 134, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5317) },
                    { 176, 40, 110, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5332) },
                    { 177, 93, 17, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5397) },
                    { 178, 47, 92, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5413) },
                    { 179, 8, 175, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5427) },
                    { 180, 38, 60, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5443) },
                    { 181, 60, 155, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5457) },
                    { 182, 62, 122, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5473) },
                    { 183, 1, 15, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5487) },
                    { 184, 69, 178, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5502) },
                    { 185, 36, 44, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5516) },
                    { 186, 6, 20, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5531) },
                    { 187, 86, 193, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5545) },
                    { 188, 44, 107, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5560) },
                    { 189, 65, 38, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5574) },
                    { 190, 82, 97, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5590) },
                    { 191, 61, 68, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5603) },
                    { 192, 98, 97, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5618) },
                    { 193, 41, 185, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5632) },
                    { 194, 99, 68, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5647) },
                    { 195, 61, 104, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5661) },
                    { 196, 98, 16, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5676) },
                    { 197, 77, 55, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5723) },
                    { 198, 42, 30, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5738) },
                    { 199, 44, 36, new DateTime(2025, 2, 3, 18, 20, 21, 363, DateTimeKind.Utc).AddTicks(5752) }
                });

            migrationBuilder.CreateIndex(
                name: "PK",
                schema: "Products",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                schema: "Products",
                table: "Products",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders",
                schema: "Products");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Products");
        }
    }
}
