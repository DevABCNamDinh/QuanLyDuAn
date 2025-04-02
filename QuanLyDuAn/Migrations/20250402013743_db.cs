using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyDuAn.Migrations
{
    /// <inheritdoc />
    public partial class db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "ImagePath", "Price", "PublishedDate", "Quantity", "Title" },
                values: new object[,]
                {
                    { new Guid("68c25d7d-36a2-4a40-a4ce-6aa9df56e195"), "Nhiều Tác Giả", "MM", "https://cdn1.fahasa.com/media/wysiwyg/HONG_KD/Frame_T4.png", 69000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Dinh Dưỡng Cho Bệnh Nhân Ung Thư" },
                    { new Guid("841b6fe5-efb2-4efe-b94f-4da5cdcadd6c"), "Desmond Tutu, Douglas Abrams, Tenzin Gyatso", "MM", "https://cdn1.fahasa.com/media/catalog/product/i/m/image_190558.jpg", 97500m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Hỷ lạc từ tâm" },
                    { new Guid("ad06202f-fbf9-4e6c-96e6-700c20901228"), "Thích Nhất Hạnh", "MM", "https://cdn1.fahasa.com/media/catalog/product/h/a/hanh-phuc-cam-tay---bia-1-_tb-2024_.jpg", 65000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Hạnh Phúc Cầm Tay" },
                    { new Guid("f1beddad-df09-4c4e-be6b-44e886ffc7c6"), "Thích Nhất Hạnh", "MM", "https://cdn1.fahasa.com/media/catalog/product/8/9/8935278607311.jpg", 470000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Không diệt không sinh đừng sợ hãi" },
                    { new Guid("fa0672ea-27fc-4aa1-ae06-3718e661bdce"), "Kate Crawford", "MM", "https://cdn1.fahasa.com/media/catalog/product/n/x/nxbtre_full_04012024_030139.jpg", 100000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Bản Đồ AI " }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_BookId",
                table: "CartItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
