using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchMvc.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image,CategoryId) " +
            "VALUES('Caderno Espiral', 'Caderno Espiral 100 folhas', 7.45, 50,'caderno1.jpg', 1)");

            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image,CategoryId) " +
            "VALUES('Estojo', 'Estojo escolar cinza', 5.65, 70,'estojo1.jpg', 1)");

            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image,CategoryId)" +
            "VALUES('Borracha', 'Borracha escolar branca', 5.50, 100,'borracha1.jpg', 1)");

            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image,CategoryId)" +
            "VALUES('Calculadora', 'Calculadora científica', 210.00, 20,'calculadora1.jpg', 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}
