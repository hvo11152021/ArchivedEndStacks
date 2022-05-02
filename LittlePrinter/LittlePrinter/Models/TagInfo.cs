using System.ComponentModel.DataAnnotations;

namespace LittlePrinter.Models
{
    public class TagInfo
    {
        public int ID { get; set; }

        [Display(Name = "Carton Number")]
        [Required]
        public int CartonNumber { get; set; }

        [Display(Name = "Buyer Carton Number")]
        [Required]
        public int BuyerCartonNumber { get; set; }

        [Display(Name = "Style PPJ")]
        public string StylePPJ { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Fab")]
        public string Fab { get; set; }

        [Display(Name = "Color Name")]
        public string ColorName { get; set; }

        public int Col000 { get; set; }
        public int Col00 { get; set; }
        public int Col0 { get; set; }
        public int Col1 { get; set; }
        public int Col2 { get; set; }
        public int Col3 { get; set; }
        public int Col4 { get; set; }
        public int Col5 { get; set; }
        public int Col6 { get; set; }
        public int Col7 { get; set; }
        public int Col8 { get; set; }
        public int Col9 { get; set; }
        public int Col10 { get; set; }
        public int Col11 { get; set; }
        public int Col12 { get; set; }
        public int Col13 { get; set; }
        public int Col14 { get; set; }
        public int Col15 { get; set; }
        public int Col16 { get; set; }
        public int Col17 { get; set; }
        public int Col18 { get; set; }
        public int Col19 { get; set; }
        public int Col20 { get; set; }
        public int Col21 { get; set; }
        public int Col22 { get; set; }
        public int Col23 { get; set; }
        public int Col24 { get; set; }
        public int Col25 { get; set; }
        public int Col26 { get; set; }
        public int Col27 { get; set; }
        public int Col28 { get; set; }
        public int Col29 { get; set; }
        public int Col30 { get; set; }
        public int Col31 { get; set; }
        public int Col32 { get; set; }
        public int Col33 { get; set; }
        public int Col34 { get; set; }
        public int Col35 { get; set; }
        public int Col36 { get; set; }
        public int Col37 { get; set; }
        public int Col38 { get; set; }
        public int Col39 { get; set; }
        public int Col40 { get; set; }
        public int Col41 { get; set; }
        public int Col42 { get; set; }
        public int Col43 { get; set; }
        public int Col44 { get; set; }
        public int Col45 { get; set; }
        public int Col46 { get; set; }
        public int Col47 { get; set; }
        public int Col48 { get; set; }
        public int Col49 { get; set; }
        public int Col50 { get; set; }
        public int Col51 { get; set; }
        public int Col52 { get; set; }
        public int Col53 { get; set; }
        public int Col54 { get; set; }
        public int Col55 { get; set; }
        public int Col56 { get; set; }
        public int Col57 { get; set; }
        public int Col58 { get; set; }
        public int Col59 { get; set; }
        public int Col60 { get; set; }

        public int Size2XS { get; set; }
        public int SizeXS { get; set; }
        public int SizeS { get; set; }
        public int SizeM { get; set; }
        public int SizeL { get; set; }
        public int SizeXL { get; set; }
        public int Size2XL { get; set; }
        public int Size3XL { get; set; }

        public int SizeX { get; set; }
        public int SizeX1 { get; set; }
        public int SizeX2 { get; set; }
        public int SizeX3 { get; set; }
        public int SizeLL { get; set; }
        public int Size3L { get; set; }
        public int Size4L { get; set; }
        public int Size5L { get; set; }

        [Display(Name = "Total Quantity")]
        [Required]
        public int TotalQuantity { get; set; }

        [Display(Name = "Carton Quantity")]
        [Required]
        public int CartonQuantity { get; set; }

        [Display(Name = "Total Pieces")]
        [Required]
        public int TotalPieces { get; set; }

        [Display(Name = "Total Net Weight")]
        [Required]
        public double TotalNetWeight { get; set; }

        [Display(Name = "Total Gross Weight")]
        [Required]
        public double TotalGrossWeight { get; set; }

        [Display(Name = "Dimension")]
        [Required]
        public string Dimension { get; set; }
    }
}
