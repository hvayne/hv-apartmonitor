using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApartmentCrawler.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    ParentId = table.Column<string>(type: "TEXT", nullable: true),
                    MaklerId = table.Column<string>(type: "TEXT", nullable: true),
                    HasLogo = table.Column<string>(type: "TEXT", nullable: true),
                    MaklerName = table.Column<string>(type: "TEXT", nullable: true),
                    LocId = table.Column<string>(type: "TEXT", nullable: true),
                    StreetAddress = table.Column<string>(type: "TEXT", nullable: true),
                    YardSize = table.Column<string>(type: "TEXT", nullable: true),
                    YardSizeTypeId = table.Column<string>(type: "TEXT", nullable: true),
                    SubmissionId = table.Column<string>(type: "TEXT", nullable: true),
                    AdtypeId = table.Column<string>(type: "TEXT", nullable: true),
                    ProductTypeId = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<string>(type: "TEXT", nullable: true),
                    Photo = table.Column<string>(type: "TEXT", nullable: true),
                    PhotoVer = table.Column<string>(type: "TEXT", nullable: true),
                    PhotosCount = table.Column<string>(type: "TEXT", nullable: true),
                    AreaSizeValue = table.Column<string>(type: "TEXT", nullable: true),
                    VideoUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CurrencyId = table.Column<string>(type: "TEXT", nullable: true),
                    OrderDate = table.Column<string>(type: "TEXT", nullable: true),
                    PriceTypeId = table.Column<string>(type: "TEXT", nullable: true),
                    Vip = table.Column<string>(type: "TEXT", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    EstateTypeId = table.Column<string>(type: "TEXT", nullable: true),
                    AreaSize = table.Column<string>(type: "TEXT", nullable: true),
                    AreaSizeTypeId = table.Column<string>(type: "TEXT", nullable: true),
                    MapLat = table.Column<string>(type: "TEXT", nullable: true),
                    MapLon = table.Column<string>(type: "TEXT", nullable: true),
                    LLiving = table.Column<string>(type: "TEXT", nullable: true),
                    SpecialPersons = table.Column<string>(type: "TEXT", nullable: true),
                    Rooms = table.Column<string>(type: "TEXT", nullable: true),
                    Bedrooms = table.Column<string>(type: "TEXT", nullable: true),
                    Floor = table.Column<string>(type: "TEXT", nullable: true),
                    ParkingId = table.Column<string>(type: "TEXT", nullable: true),
                    Canalization = table.Column<string>(type: "TEXT", nullable: true),
                    Water = table.Column<string>(type: "TEXT", nullable: true),
                    Road = table.Column<string>(type: "TEXT", nullable: true),
                    Electricity = table.Column<string>(type: "TEXT", nullable: true),
                    OwnerTypeId = table.Column<string>(type: "TEXT", nullable: true),
                    OsmId = table.Column<string>(type: "TEXT", nullable: true),
                    NameJson = table.Column<string>(type: "TEXT", nullable: true),
                    PathwayJson = table.Column<string>(type: "TEXT", nullable: true),
                    HomeSelfie = table.Column<string>(type: "TEXT", nullable: true),
                    SeoTitleJson = table.Column<string>(type: "TEXT", nullable: true),
                    SeoNameJson = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    GenderId = table.Column<string>(type: "TEXT", nullable: true),
                    PersonalDataAgreement = table.Column<string>(type: "TEXT", nullable: true),
                    AgreeTBCTerms = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
