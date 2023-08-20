using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocilaMediaProject.Data.Enum;
using SocilaMediaProject.Models;

namespace SocilaMediaProject.Data
{
    public class MyDBContext : IdentityDbContext<AppUser>
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }

        public override int SaveChanges()
        {
            var Entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Modified || e.State == EntityState.Added
                           select e.Entity;

            foreach (var Entity in Entities)
            {
                ValidationContext validationContext = new(Entity);
                Validator.ValidateObject(Entity, validationContext, true);
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new Seed(modelBuilder).SeedData();
            base.OnModelCreating(modelBuilder);

        }
        

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities {get;set;}

    }

}