using Microsoft.EntityFrameworkCore.Migrations;

namespace DarGates.Migrations
{
    public partial class SeedPerHour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Owner",
                columns: new[] { "Type", "Price", "PriceInHoliday" },
                values: new object[,]
                {
                    {
                        "المحاسبه بالساعه",
                        "5.0",
                        "10.0" 
                    }
                }) ;
        }
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From AspNetRoles where Type = 'المحاسبه بالساعه' ", true);
        }
    }
}
