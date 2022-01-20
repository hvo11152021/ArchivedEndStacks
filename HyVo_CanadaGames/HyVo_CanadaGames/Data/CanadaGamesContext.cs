using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HyVo_CanadaGames.Models;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace HyVo_CanadaGames.Data
{
    public class CanadaGamesContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string UserName
        {
            get; private set;
        }

        public CanadaGamesContext(DbContextOptions<CanadaGamesContext> options) 
            : base(options)
        {
            UserName = "SeedData";
        }

        public CanadaGamesContext(DbContextOptions<CanadaGamesContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            UserName = _httpContextAccessor.HttpContext?.User.Identity.Name;
            UserName = UserName ?? "Unknown";
        }

        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Contingent> Contingents { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Sport> Sports { get; set; }

        public DbSet<AthleteSport> AthleteSports { get; set; }

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

            //For athletesport
            modelBuilder.Entity<AthleteSport>().HasKey(a => new { a.SportID, a.AthleteID });

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

            //Prevent cascade delete in athletesport
            modelBuilder.Entity<AthleteSport>()
                .HasOne(a => a.Athlete)
                .WithMany(s => s.AthleteSports)
                .HasForeignKey(fk => fk.AthleteID)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditable trackable)
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;

                        case EntityState.Added:
                            trackable.CreatedOn = now;
                            trackable.CreatedBy = UserName;
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;
                    }
                }
            }
        }
    }
}
