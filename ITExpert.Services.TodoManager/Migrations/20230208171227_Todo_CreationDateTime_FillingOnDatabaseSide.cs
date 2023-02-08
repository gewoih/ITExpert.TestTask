using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITExpert.Services.TodoManager.Migrations
{
    /// <inheritdoc />
    public partial class TodoCreationDateTimeFillingOnDatabaseSide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Todos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2023, 2, 8, 17, 11, 10, 813, DateTimeKind.Utc).AddTicks(9856));

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDateTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDateTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDateTime",
                table: "Todos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 8, 17, 11, 10, 813, DateTimeKind.Utc).AddTicks(9856),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDateTime",
                value: new DateTime(2023, 2, 8, 17, 11, 10, 813, DateTimeKind.Utc).AddTicks(9856));

            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDateTime",
                value: new DateTime(2023, 2, 8, 17, 11, 10, 813, DateTimeKind.Utc).AddTicks(9856));
        }
    }
}
