using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LittlePrinter.Data
{
    public static class TBSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LittlePrinterContext(serviceProvider.GetRequiredService<DbContextOptions<LittlePrinterContext>>()))
            {
                //if (!context.CartonLabels.Any())
                //{
                //    var contingents = new List<CartonLabel>
                //    {
                //        new CartonLabel {
                //            ID = 1,
                //            CartonNumber = 4,
                //            BuyerCartonNumber = 1,
                //            StylePPJ = "2165",
                //            Brand = "Torrid",
                //            Description = "Women's pants",
                //            Fab = "",
                //            ColorName = "OLIVE",
                //            Col00 = 0,
                //            Col0 = 0,
                //            Col1 = 0,
                //            Col2 = 0,
                //            Col3 = 0,
                //            Col4 = 0,
                //            Col5 = 0,
                //            Col6 = 0,
                //            Col8 = 0,
                //            Col10 = 4,
                //            Col12 = 4,
                //            Col14 = 0,
                //            Col16 = 0,
                //            Col18 = 0,
                //            Col20 = 0,
                //            Col22 = 20,
                //            Col24 = 0,
                //            Col26 = 0,
                //            Col28 = 3,
                //            Col30 = 7,
                //            TotalQuantity = 38,
                //            CartonQuantity = 1,
                //            TotalPieces = 38,
                //            TotalNetWeight = 22,
                //            TotalGrossWeight = 23,
                //            Dimension = "52x36x40"
                //        }
                //    };
                //    context.CartonLabels.AddRange(contingents);
                //    context.SaveChanges();
                //}
            }
        }
    }
}
