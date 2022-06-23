using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace projektApbd.Shared.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e => 
            {
                e.HasKey(e => e.Id);

                e.Property(e => e.Username).HasMaxLength(15).IsRequired();
                e.Property(e => e.Password).HasMaxLength(255).IsRequired();
                e.Property(e => e.Email).HasMaxLength(320).IsRequired();

                e.ToTable("Users");
            });

            modelBuilder.Entity<Company>(e => 
            {
                e.HasKey(e => e.Id);

                e.Property(e => e.Listdate).IsRequired();
                e.Property(e => e.Ticker).HasMaxLength(10).IsRequired();
                e.Property(e => e.Name).HasMaxLength(255).IsRequired();
                e.Property(e => e.Url).HasMaxLength(255).IsRequired();
                e.Property(e => e.Country).HasMaxLength(255).IsRequired();
                e.Property(e => e.Industry).HasMaxLength(255).IsRequired();
                e.Property(e => e.Sector).HasMaxLength(255).IsRequired();
                e.Property(e => e.Logo).HasMaxLength(255).IsRequired();
                e.Property(e => e.Employees).IsRequired();
                e.Property(e => e.Phone).HasMaxLength(20).IsRequired();
                e.Property(e => e.Ceo).HasMaxLength(255).IsRequired();
                e.Property(e => e.Description).HasMaxLength(4000).IsRequired();
                e.Property(e => e.Exchange).HasMaxLength(3).IsRequired();
                e.Property(e => e.HqAddress).HasMaxLength(255).IsRequired(false);
                e.Property(e => e.HqState).HasMaxLength(3).IsRequired(false);
                e.Property(e => e.HqCountry).HasMaxLength(3).IsRequired(false);
                e.Property(e => e.Active).IsRequired();

                e.ToTable("Companies");
            });

            modelBuilder.Entity<UserCompany>(e => 
            {
                e.HasKey(e => new
                {   
                    e.UserId,
                    e.CompanyId
                });

                e.HasOne(e => e.User)
                .WithMany(e => e.UserCompanies)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(e => e.Company)
                .WithMany(e => e.UserCompanies)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

                e.ToTable("UserCompany");
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString(""));
        }
    }
}