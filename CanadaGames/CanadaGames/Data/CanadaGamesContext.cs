using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanadaGames.Models;

namespace CanadaGames.Data
{
    public class CanadaGamesContext : DbContext
    {
        public CanadaGamesContext(DbContextOptions<CanadaGamesContext> options) : base(options) { }

        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Contingent> Contingents { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Sport> Sports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("CG");

            //Unique Constraints
            modelBuilder.Entity<Athlete>()
                .HasIndex(a => a.AthleteCode)
                .IsUnique();

            modelBuilder.Entity<Contingent>()
                .HasIndex(c => c.Code)
                .IsUnique();

            modelBuilder.Entity<Gender>()
                .HasIndex(g => g.Code)
                .IsUnique();

            modelBuilder.Entity<Sport>()
                .HasIndex(s => s.Code)
                .IsUnique();

            /////////////////////////////////////////

            //Prevent Cascade Deletes
            modelBuilder.Entity<Coach>()
                .HasMany<Athlete>(d => d.Athletes)
                .WithOne(p => p.Coach)
                .HasForeignKey(p => p.CoachID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contingent>()
                .HasMany<Athlete>(d => d.Athletes)
                .WithOne(p => p.Contingent)
                .HasForeignKey(p => p.ContingentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Gender>()
                .HasMany<Athlete>(d => d.Athletes)
                .WithOne(p => p.Gender)
                .HasForeignKey(p => p.GenderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sport>()
                .HasMany<Athlete>(d => d.Athletes)
                .WithOne(p => p.Sport)
                .HasForeignKey(p => p.SportID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
