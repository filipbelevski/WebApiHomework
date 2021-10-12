using Domain.Models;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.UserName).IsRequired().HasMaxLength(50);
                x.Property(x => x.FullName).IsRequired().HasMaxLength(50);
                x.Property(x => x.Password).IsRequired();
                x.Property(x => x.Password).IsRequired();
                x.HasMany(x => x.Movies);
            });

            builder.Entity<Movie>(x =>
            {
                x.HasKey(x => x.Id);
                x.Property(x => x.Title).IsRequired();
                x.Property(x => x.Year).IsRequired();
                x.HasMany(x => x.Users);
            });

        }
    }
}
