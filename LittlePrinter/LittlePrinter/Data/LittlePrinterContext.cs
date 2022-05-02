using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LittlePrinter.Models;

    public class LittlePrinterContext : DbContext
    {
        public LittlePrinterContext (DbContextOptions<LittlePrinterContext> options)
            : base(options)
        {
        }

        public DbSet<LittlePrinter.Models.Tag> Tag { get; set; }
    }
