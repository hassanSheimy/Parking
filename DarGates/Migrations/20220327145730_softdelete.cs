using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarGates.Migrations
{
    public partial class softdelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserId",
                table: "PrinterLog",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PrinterLog",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserId",
                table: "Owner",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Owner",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DeleteTime",
                table: "GateLog",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserId",
                table: "GateLog",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GateLog",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PrinterLog_DeletedByUserId",
                table: "PrinterLog",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Owner_DeletedByUserId",
                table: "Owner",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_GateLogId",
                table: "Invitation",
                column: "GateLogId");

            migrationBuilder.CreateIndex(
                name: "IX_GateLog_DeletedByUserId",
                table: "GateLog",
                column: "DeletedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GateLog_AspNetUsers_DeletedByUserId",
                table: "GateLog",
                column: "DeletedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_GateLog_GateLogId",
                table: "Invitation",
                column: "GateLogId",
                principalTable: "GateLog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Owner_AspNetUsers_DeletedByUserId",
                table: "Owner",
                column: "DeletedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrinterLog_AspNetUsers_DeletedByUserId",
                table: "PrinterLog",
                column: "DeletedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GateLog_AspNetUsers_DeletedByUserId",
                table: "GateLog");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_GateLog_GateLogId",
                table: "Invitation");

            migrationBuilder.DropForeignKey(
                name: "FK_Owner_AspNetUsers_DeletedByUserId",
                table: "Owner");

            migrationBuilder.DropForeignKey(
                name: "FK_PrinterLog_AspNetUsers_DeletedByUserId",
                table: "PrinterLog");

            migrationBuilder.DropIndex(
                name: "IX_PrinterLog_DeletedByUserId",
                table: "PrinterLog");

            migrationBuilder.DropIndex(
                name: "IX_Owner_DeletedByUserId",
                table: "Owner");

            migrationBuilder.DropIndex(
                name: "IX_Invitation_GateLogId",
                table: "Invitation");

            migrationBuilder.DropIndex(
                name: "IX_GateLog_DeletedByUserId",
                table: "GateLog");

            

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "PrinterLog");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PrinterLog");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Owner");

            migrationBuilder.DropColumn(
                name: "GateLogId",
                table: "Invitation");

            

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "GateLog");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GateLog");

            
        }
    }
}
