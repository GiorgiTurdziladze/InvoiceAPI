using System;
using DeveloperTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DeveloperTest
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {
            base.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }

        public DbSet<Invoice> Inovieces { get; set; }


        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>().HasData(new Invoice()
            {
                ID = 1,
                Amount = 100,
                IsPaid = false,
                DateCreated = DateTime.Now,
                DateChanged = null,
                DateDeleted = null
            });
            modelBuilder.Entity<Invoice>().HasData(new Invoice()
            {
                ID = 2,
                Amount = 150,
                IsPaid = true,
                DateCreated = DateTime.Now,
                DateChanged = null,
                DateDeleted = null
            });
            modelBuilder.Entity<Invoice>().HasData(new Invoice()
            {
                ID = 3,
                Amount = 200,
                IsPaid = false,
                DateCreated = DateTime.Now,
                DateChanged = null,
                DateDeleted = null
            });
        }
    }
}

