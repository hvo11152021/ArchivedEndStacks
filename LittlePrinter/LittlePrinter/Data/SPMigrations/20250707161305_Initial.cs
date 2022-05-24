using Microsoft.EntityFrameworkCore.Migrations;

namespace LittlePrinter.Data.SPMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartonNumber = table.Column<int>(nullable: false),
                    BuyerCartonNumber = table.Column<int>(nullable: false),
                    StylePPJ = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Fab = table.Column<string>(nullable: true),
                    ColorName = table.Column<string>(nullable: true),
                    Col000 = table.Column<int>(nullable: false),
                    Col00 = table.Column<int>(nullable: false),
                    Col0 = table.Column<int>(nullable: false),
                    Col1 = table.Column<int>(nullable: false),
                    Col2 = table.Column<int>(nullable: false),
                    Col3 = table.Column<int>(nullable: false),
                    Col4 = table.Column<int>(nullable: false),
                    Col5 = table.Column<int>(nullable: false),
                    Col6 = table.Column<int>(nullable: false),
                    Col7 = table.Column<int>(nullable: false),
                    Col8 = table.Column<int>(nullable: false),
                    Col9 = table.Column<int>(nullable: false),
                    Col10 = table.Column<int>(nullable: false),
                    Col11 = table.Column<int>(nullable: false),
                    Col12 = table.Column<int>(nullable: false),
                    Col13 = table.Column<int>(nullable: false),
                    Col14 = table.Column<int>(nullable: false),
                    Col15 = table.Column<int>(nullable: false),
                    Col16 = table.Column<int>(nullable: false),
                    Col17 = table.Column<int>(nullable: false),
                    Col18 = table.Column<int>(nullable: false),
                    Col19 = table.Column<int>(nullable: false),
                    Col20 = table.Column<int>(nullable: false),
                    Col21 = table.Column<int>(nullable: false),
                    Col22 = table.Column<int>(nullable: false),
                    Col23 = table.Column<int>(nullable: false),
                    Col24 = table.Column<int>(nullable: false),
                    Col25 = table.Column<int>(nullable: false),
                    Col26 = table.Column<int>(nullable: false),
                    Col27 = table.Column<int>(nullable: false),
                    Col28 = table.Column<int>(nullable: false),
                    Col29 = table.Column<int>(nullable: false),
                    Col30 = table.Column<int>(nullable: false),
                    Col31 = table.Column<int>(nullable: false),
                    Col32 = table.Column<int>(nullable: false),
                    Col33 = table.Column<int>(nullable: false),
                    Col34 = table.Column<int>(nullable: false),
                    Col35 = table.Column<int>(nullable: false),
                    Col36 = table.Column<int>(nullable: false),
                    Col37 = table.Column<int>(nullable: false),
                    Col38 = table.Column<int>(nullable: false),
                    Col39 = table.Column<int>(nullable: false),
                    Col40 = table.Column<int>(nullable: false),
                    Col41 = table.Column<int>(nullable: false),
                    Col42 = table.Column<int>(nullable: false),
                    Col43 = table.Column<int>(nullable: false),
                    Col44 = table.Column<int>(nullable: false),
                    Col45 = table.Column<int>(nullable: false),
                    Col46 = table.Column<int>(nullable: false),
                    Col47 = table.Column<int>(nullable: false),
                    Col48 = table.Column<int>(nullable: false),
                    Col49 = table.Column<int>(nullable: false),
                    Col50 = table.Column<int>(nullable: false),
                    Col51 = table.Column<int>(nullable: false),
                    Col52 = table.Column<int>(nullable: false),
                    Col53 = table.Column<int>(nullable: false),
                    Col54 = table.Column<int>(nullable: false),
                    Col55 = table.Column<int>(nullable: false),
                    Col56 = table.Column<int>(nullable: false),
                    Col57 = table.Column<int>(nullable: false),
                    Col58 = table.Column<int>(nullable: false),
                    Col59 = table.Column<int>(nullable: false),
                    Col60 = table.Column<int>(nullable: false),
                    Size2XS = table.Column<int>(nullable: false),
                    SizeXS = table.Column<int>(nullable: false),
                    SizeS = table.Column<int>(nullable: false),
                    SizeM = table.Column<int>(nullable: false),
                    SizeL = table.Column<int>(nullable: false),
                    SizeXL = table.Column<int>(nullable: false),
                    Size2XL = table.Column<int>(nullable: false),
                    Size3XL = table.Column<int>(nullable: false),
                    SizeX = table.Column<int>(nullable: false),
                    SizeX1 = table.Column<int>(nullable: false),
                    SizeX2 = table.Column<int>(nullable: false),
                    SizeX3 = table.Column<int>(nullable: false),
                    SizeLL = table.Column<int>(nullable: false),
                    Size3L = table.Column<int>(nullable: false),
                    Size4L = table.Column<int>(nullable: false),
                    Size5L = table.Column<int>(nullable: false),
                    TotalQuantity = table.Column<int>(nullable: false),
                    CartonQuantity = table.Column<int>(nullable: false),
                    TotalPieces = table.Column<int>(nullable: false),
                    TotalNetWeight = table.Column<double>(nullable: false),
                    TotalGrossWeight = table.Column<double>(nullable: false),
                    Dimension = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
