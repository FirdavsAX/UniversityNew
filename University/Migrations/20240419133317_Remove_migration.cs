﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Migrations
{
    /// <inheritdoc />
    public partial class Remove_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureFilePath",
                table: "Course");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) 
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureFilePath",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
