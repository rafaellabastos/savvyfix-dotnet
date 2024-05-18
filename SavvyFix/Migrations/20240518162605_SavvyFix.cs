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
                name: "Endereco",
                columns: table => new
                {
                    IdEndereco = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EstadoEndereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CepEndereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NumEndereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    BairroEndereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CidadeEndereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RuaEndereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Pais = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.IdEndereco);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    IdProd = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PrecoFixo = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    MarcaProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DescProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NmProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.IdProd);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdEndereco = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    CpfClie = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NmClie = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SenhaClie = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IdEnderecoNavigationIdEndereco = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                    table.ForeignKey(
                        name: "FK_Clientes_Endereco_IdEnderecoNavigationIdEndereco",
                        column: x => x.IdEnderecoNavigationIdEndereco,
                        principalTable: "Endereco",
                        principalColumn: "IdEndereco");
                });

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
                    PrecoVariado = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    QntdProcura = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdCliente = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.IdAtividades);
                    table.ForeignKey(
                        name: "FK_Atividades_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente");
                });

            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    IdCliente = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    QntdProd = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ValorCompra = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    IdCompra = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    IdProd = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    PrecoVariado = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    EspecificacaoProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NmProd = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    IdClienteNavigationIdCliente = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    IdProdNavigationIdProd = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    PrecoVariadoNavigationIdAtividades = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra", x => x.IdCliente);
                    table.ForeignKey(
                        name: "FK_Compra_Atividades_PrecoVariadoNavigationIdAtividades",
                        column: x => x.PrecoVariadoNavigationIdAtividades,
                        principalTable: "Atividades",
                        principalColumn: "IdAtividades");
                    table.ForeignKey(
                        name: "FK_Compra_Clientes_IdClienteNavigationIdCliente",
                        column: x => x.IdClienteNavigationIdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_Compra_Produtos_IdProdNavigationIdProd",
                        column: x => x.IdProdNavigationIdProd,
                        principalTable: "Produtos",
                        principalColumn: "IdProd");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_IdCliente",
                table: "Atividades",
                column: "IdCliente",
                unique: true,
                filter: "\"IdCliente\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IdEnderecoNavigationIdEndereco",
                table: "Clientes",
                column: "IdEnderecoNavigationIdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_IdClienteNavigationIdCliente",
                table: "Compra",
                column: "IdClienteNavigationIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_IdProdNavigationIdProd",
                table: "Compra",
                column: "IdProdNavigationIdProd");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_PrecoVariadoNavigationIdAtividades",
                table: "Compra",
                column: "PrecoVariadoNavigationIdAtividades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Endereco");
        }
    }
}
