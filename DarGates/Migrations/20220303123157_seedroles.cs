using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DarGates.Migrations
{
    public partial class seedroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                    { Guid.NewGuid().ToString() , "Guard", "GUARD" ,Guid.NewGuid().ToString()},
                    { Guid.NewGuid().ToString() , "Manager", "MANAGER" ,Guid.NewGuid().ToString()},
                    { Guid.NewGuid().ToString() , "Admin", "ADMIN" ,Guid.NewGuid().ToString()}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From AspNetRoles", true);
        }
    }
}
