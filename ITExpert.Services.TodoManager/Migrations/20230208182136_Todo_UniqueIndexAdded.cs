using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITExpert.Services.TodoManager.Migrations
{
    /// <inheritdoc />
    public partial class TodoUniqueIndexAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Todos_Title_Category",
                table: "Todos",
                columns: new[] { "Title", "Category" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Todos_Title_Category",
                table: "Todos");
        }
    }
}
