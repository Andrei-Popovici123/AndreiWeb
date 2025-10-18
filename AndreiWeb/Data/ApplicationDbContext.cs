using AndreiWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace AndreiWeb.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Roles> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
         new Category{Id = 1, Name = "Action", DisplayOrder = 1},
         new Category{Id = 2, Name = "SciFi", DisplayOrder = 2},
         new Category{Id = 3, Name = "History", DisplayOrder = 3}
            );
        modelBuilder.Entity<Roles>()
            .Property(r => r.Name)
            .HasConversion<string>();
        modelBuilder.Entity<Roles>().HasData(
            new Roles { Id = 1, Name = RoleName.Admin},
            new Roles { Id = 2, Name = RoleName.Customer},
            new Roles { Id = 3, Name = RoleName.User}
        );
    }
}