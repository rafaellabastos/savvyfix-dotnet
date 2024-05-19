using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavvyFix.Migrations
{
    /// <inheritdoc />
    public partial class SavvyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atividades",
                columns: table => new
                {
                    IdAtividades = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ClimaAtual = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DemandaProduto = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    HorarioAtual = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    LocalizacaoAtual = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PrecoVariado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QntdProcura = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdCliente = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.IdAtividades);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CpfClie = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    NmClie = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SenhaClie = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CepEndereco = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    RuaEndereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NumEndereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    IdCompra = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    QntdProd = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ValorCompra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdProd = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    NmProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.IdCompra);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    IdProd = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PrecoFixo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarcaProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DescProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NmProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Img = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.IdProd);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
