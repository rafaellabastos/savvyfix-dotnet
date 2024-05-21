using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavvyFix.Migrations
{
    /// <inheritdoc />
    public partial class EditAtividades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Atividades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdCliente",
                table: "Atividades",
                type: "NUMBER(19)",
                nullable: true);
        }
    }
}
