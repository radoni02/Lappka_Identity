using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("40df127e-e36a-4564-8cd3-231f4ed75a0a"), "129e3f49-7902-4637-8454-e02f5feab143", "User", "USER" },
                    { new Guid("90e5bcf9-36e8-4dd8-92ff-9e205549dd37"), "40d8d2ce-a965-49b5-8283-a60809c3fd97", "Worker", "WORKER" },
                    { new Guid("b6cd9ff6-4778-43ea-8e9b-105f7fc5dc57"), "ef0233ab-fbed-4479-8869-29c687966a42", "SuperAdmin", "SUPERADMIN" },
                    { new Guid("bededc0e-4850-4d1a-b683-bf3b03d30b81"), "634e9974-3746-4556-9451-a4e635b4e578", "Admin", "ADMIN" },
                    { new Guid("eb894bda-cd60-4d9f-a14a-81e0f6e864b6"), "a400e99f-07fb-4df3-a0a0-3a6c55f839a7", "Shelter", "SHELTER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("1f4a577b-d3f0-458b-83f0-c12f28df4f1f"), 0, "873828bc-1ca4-4779-b4f3-1f33ffe38c69", "admin@admin.com", false, null, null, false, null, "ADMIN@ADMIN.COM", "SUPERADMIN", null, null, false, null, "HNHBNE7CVLFYUOEZHQK7X74TP5F5KNBD", false, "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("b6cd9ff6-4778-43ea-8e9b-105f7fc5dc57"), new Guid("1f4a577b-d3f0-458b-83f0-c12f28df4f1f") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("40df127e-e36a-4564-8cd3-231f4ed75a0a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("90e5bcf9-36e8-4dd8-92ff-9e205549dd37"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bededc0e-4850-4d1a-b683-bf3b03d30b81"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eb894bda-cd60-4d9f-a14a-81e0f6e864b6"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b6cd9ff6-4778-43ea-8e9b-105f7fc5dc57"), new Guid("1f4a577b-d3f0-458b-83f0-c12f28df4f1f") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b6cd9ff6-4778-43ea-8e9b-105f7fc5dc57"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1f4a577b-d3f0-458b-83f0-c12f28df4f1f"));

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
