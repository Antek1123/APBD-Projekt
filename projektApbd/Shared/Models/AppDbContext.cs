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
        private readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(e =>
            {
                e.HasKey(e => e.Id);
                e.Property(e => e.Username).HasMaxLength(15).IsRequired();
                e.Property(e => e.Password).HasMaxLength(255).IsRequired();
                e.Property(e => e.Email).HasMaxLength(320).IsRequired();

                e.ToTable("Users");
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString(""));
        }
    }
}