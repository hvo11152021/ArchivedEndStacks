using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LittlePrinter.Models;

namespace LittlePrinter.Data
{
    public class LittlePrinterContext : DbContext
    {
        public LittlePrinterContext (DbContextOptions<LittlePrinterContext> options)
            : base(options)
        {
        }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
