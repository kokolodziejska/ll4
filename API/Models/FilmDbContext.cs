using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Shared;

namespace API.Models;

public class FilmDbContext : DbContext
{
    public DbSet<Film> films { get; set; }

    private readonly IConfiguration _configuration;


    public FilmDbContext(DbContextOptions<FilmDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Film>()
                .HasKey(p => p.Title);

        modelBuilder.Entity<Film>()
                .Property(p => p.Rating)
                .IsRequired()
                .HasColumnType("int");

        modelBuilder.Entity<Film>()
            .HasData(
                new Film { Title = "Example Film 1", Rating = 5 },
                new Film { Title = "Example Film 2", Rating = 8 }
            );
    }
}

