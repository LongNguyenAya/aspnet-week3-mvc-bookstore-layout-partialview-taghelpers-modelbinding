using Microsoft.EntityFrameworkCore;
using AspNetWeek2.Mvc.Models;
namespace AspNetWeek2.Mvc.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<BookCategory> BookCategories => Set<BookCategory>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        modelBuilder.Entity<BookCategory>().HasData(
            new BookCategory { Id = 1, Name = "Sách thiếu nhi" },
            new BookCategory { Id = 2, Name = "Sách giáo khoa" }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book 
            { 
                Id = 1, 
                Name = "Sách thiếu nhi 1", 
                Author = "Tác giả 1",      
                Price = 250000, 
                Quantity = 10, 
                MinStock = 5,            
                CategoryId = 1 
            },
            new Book 
            { 
                Id = 2, 
                Name = "Sách thiếu nhi 2", 
                Author = "Tác giả 2",
                Price = 1350000, 
                Quantity = 4, 
                MinStock = 2,
                CategoryId = 1 
            },
            new Book 
            { 
                Id = 3, 
                Name = "Sách giáo khoa 1", 
                Author = "Tác giả 3",
                Price = 3200000, 
                Quantity = 3, 
                MinStock = 1,
                CategoryId = 2 
            }
        );
    }
}