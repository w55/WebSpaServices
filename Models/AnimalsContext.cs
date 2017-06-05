using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebSpaServices.Models
{
    public class AnimalsContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        public DbSet<Skin> Skins { get; set; }

        public DbSet<Kind> Kinds { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Region> Regions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().HasMany(c => c.Regions)
                .WithMany(s => s.Animals)
                .Map(t => t.MapLeftKey("AnimalId")
                .MapRightKey("RegionId")
                .ToTable("AnimalRegions"));
        }
    }
}