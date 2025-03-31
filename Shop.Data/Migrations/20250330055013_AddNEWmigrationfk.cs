using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNEWmigrationfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Action" },
                    { 2, 2, "SciFi" },
                    { 3, 3, "Fantasy" },
                    { 4, 4, "Romance" },
                    { 5, 5, "Comedy" },
                    { 6, 6, "Horror" },
                    { 7, 7, "Mystery" },
                    { 8, 8, "Adventure" },
                    { 9, 9, "Slice of Life" },
                    { 10, 10, "Mecha" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Eiichiro Oda", 3, "Follow Monkey D. Luffy and his pirate crew in their quest to find the greatest treasure, the One Piece, and become the Pirate King.\r\n\r\nJoin them as they face formidable foes and uncover the mysteries of the Grand Line.", "SWD9999001", 99.0, 90.0, 80.0, 85.0, "One Piece" },
                    { 2, "Chugong", 1, "In a world where hunters with various magical powers battle deadly monsters, Sung Jin-Woo, the weakest of all hunters, finds himself in a never-ending struggle for survival.\r\n\r\nAs he discovers a mysterious power, he begins to level up beyond imagination.", "CAW777777701", 40.0, 30.0, 20.0, 25.0, "Solo Leveling" },
                    { 3, "Akira Toriyama", 2, "The adventures of Goku and his friends as they defend the Earth from an assortment of villains ranging from intergalactic space fighters and conquerors, unnaturally powerful androids, and nearly indestructible creatures.\r\n\r\nWitness epic battles and transformations that push the limits of power.", "RITO5555501", 55.0, 50.0, 35.0, 40.0, "Dragon Ball Z" },
                    { 4, "Masashi Kishimoto", 7, "Naruto Uzumaki, a young ninja who seeks recognition from his peers and dreams of becoming the Hokage, the village's leader and strongest ninja.\r\n\r\nFollow his journey as he overcomes challenges and forms bonds with his friends.", "WS3333333301", 70.0, 65.0, 55.0, 60.0, "Naruto" },
                    { 5, "Yūki Tabata", 2, "Asta, a young boy born without any magic power, aims to become the Wizard King to prove his strength and protect his kingdom.\r\n\r\nWith determination and hard work, he faces numerous magical threats.", "SOTJ1111111101", 30.0, 27.0, 20.0, 25.0, "Black Clover" },
                    { 6, "Tite Kubo", 5, "Ichigo Kurosaki, a teenager with the ability to see ghosts, gains the powers of a Soul Reaper and must protect the living world from evil spirits and guide the souls of the deceased to the afterlife.\r\n\r\nExperience his battles and growth as a protector of both worlds.", "FOT000000001", 25.0, 23.0, 20.0, 22.0, "Bleach" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
