using Microsoft.EntityFrameworkCore.Migrations;

namespace Udemy.TodoAppNTier.DataAccess.Migrations
{
    public partial class AddParameterandparametergroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParameterGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParameterGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parameter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ParameterGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parameter_ParameterGroup_ParameterGroupId",
                        column: x => x.ParameterGroupId,
                        principalTable: "ParameterGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parameter_ParameterGroupId",
                table: "Parameter",
                column: "ParameterGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parameter");

            migrationBuilder.DropTable(
                name: "ParameterGroup");
        }
    }
}
