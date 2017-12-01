using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CRUD_Operations_Angular.DataAccess.Entities;

namespace CRUD_Operations_Angular.DataAccess.Context
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersProjects> UsersProjects { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server = DESKTOP-A2GNBAB\SQLEXPRESS; 
                Database=AngularCRUD; 
                Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersProjects>()
                .HasKey(uProjs => new { uProjs.ProjectId, uProjs.UserId });
        }
    }
}
