using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>()
                .HasKey(p => new { p.AppUserId, p.StockId });

            builder.Entity<Portfolio>()
                .HasOne(p => p.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);

            builder.Entity<Portfolio>()
                .HasOne(p => p.Stock)
                .WithMany(s => s.Portfolios)
                .HasForeignKey(p => p.StockId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "9615c412-70ff-4340-8746-3a7469fbee6d",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "3fdfb2cc-bf3b-4c62-aea3-2a48c1294bfd"
                },
                new IdentityRole
                {
                    Id = "0a2abbbc-0189-4576-88a7-5600f47c868d",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "bfcbfef8-c8be-4a85-8126-b7ab51b94785"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}