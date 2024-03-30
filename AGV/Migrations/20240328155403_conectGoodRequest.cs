using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AGV.Migrations
{
    /// <inheritdoc />
    public partial class conectGoodRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdGoods",
                table: "Requests",
                newName: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_GoodsId",
                table: "Requests",
                column: "GoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Goods_GoodsId",
                table: "Requests",
                column: "GoodsId",
                principalTable: "Goods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Goods_GoodsId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_GoodsId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "GoodsId",
                table: "Requests",
                newName: "IdGoods");
        }
    }
}
