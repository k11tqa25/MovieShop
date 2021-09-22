using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdateMovieCastTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieCast",
                table: "MovieCast");

            migrationBuilder.DropIndex(
                name: "IX_MovieCast_MovieId",
                table: "MovieCast");

            migrationBuilder.DropColumn(
                name: "MovieCastId",
                table: "MovieCast");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieCast",
                table: "MovieCast",
                columns: new[] { "MovieId", "CastId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieCast",
                table: "MovieCast");

            migrationBuilder.AddColumn<int>(
                name: "MovieCastId",
                table: "MovieCast",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieCast",
                table: "MovieCast",
                column: "MovieCastId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCast_MovieId",
                table: "MovieCast",
                column: "MovieId");
        }
    }
}
