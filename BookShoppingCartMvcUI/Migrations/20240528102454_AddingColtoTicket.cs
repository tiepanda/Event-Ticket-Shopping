using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketShoppingCartMvcUI.Migrations
{
    /// <inheritdoc />
    public partial class AddingColtoTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discription",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Discription",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Ticket");
        }
    }
}
