using Nhom7_webTourdulich.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;  // Required for Identity types

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // public DbSet<Product> Products { get; set; }
    // public DbSet<Category> Categories { get; set; }
    // public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
}
