using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoList.Migrations
{
    public partial class abc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UsersUserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UsersUserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "Tasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UsersUserId",
                table: "Tasks",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UsersUserId",
                table: "Tasks",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
