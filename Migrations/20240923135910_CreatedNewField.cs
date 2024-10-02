using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TODO.API.Migrations
{
    /// <inheritdoc />
    public partial class CreatedNewField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTask",
                table: "TODOTable",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateTask",
                table: "TODOTable");
        }
    }
}
