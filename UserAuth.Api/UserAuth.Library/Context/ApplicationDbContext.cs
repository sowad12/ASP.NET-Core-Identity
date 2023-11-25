using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAuth.Library.Models.Entites;

namespace UserAuth.Library.Context
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            SeedRole(modelBuilder);
        }
        private static void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name="Admin", NormalizedName ="Admin", ConcurrencyStamp ="1"},
                new IdentityRole() { Name="User", NormalizedName ="User", ConcurrencyStamp ="2"},
                new IdentityRole() { Name="Moderator", NormalizedName = "Moderator", ConcurrencyStamp ="3"}
                );
        }
        public DbSet<User> User { get; set; }

    }
}
