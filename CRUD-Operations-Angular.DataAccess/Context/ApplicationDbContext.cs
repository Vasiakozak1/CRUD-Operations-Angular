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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server = tcp:wondrmakr.database.windows.net, 1433; 
                Initial Catalog = CRUDOperationsAng; Persist Security Info = False; 
                User ID = WondrMakr_admin; Password = AppDbStorage1; 
                MultipleActiveResultSets = False; Encrypt = True; 
                TrustServerCertificate = False; Connection Timeout = 30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersProjects>()
                .HasKey(uProjs => new { uProjs.ProjectId, uProjs.UserId });
        }
    }
}
