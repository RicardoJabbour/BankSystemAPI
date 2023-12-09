﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class TransactionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transactions");
        }
    }
}
