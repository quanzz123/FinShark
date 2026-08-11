using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Portfolio>().HasKey(p => new { p.AppUserId, p.StockId });

            builder.Entity<Portfolio>()
                .HasOne(p => p.appUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);

            builder.Entity<Portfolio>()
                .HasOne(p => p.stock)
                .WithMany(s => s.Portfolios)
                .HasForeignKey(p => p.StockId);
            List<IdentityRole> roles  = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = "647dba8b-87b6-482e-9774-c4afa611fa80",
                    Name = "Admin",
                    NormalizedName = "ADMIN"

                },

                new IdentityRole
                {
                    Id = "3cf56bb3-fb14-4b2a-86cf-349ea8c065eb",
                    Name = "User",
                    NormalizedName = "USER"

                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
       
    
}