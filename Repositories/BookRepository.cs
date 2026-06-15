namespace AspNetWeek2.Mvc.Repositories;
using AspNetWeek2.Mvc.Models;
using Microsoft.EntityFrameworkCore;
using AspNetWeek2.Mvc.Data;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;
    
    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Book>> GetAllAsync()
        => _context.Books.Include(p => p.Category).ToListAsync();

    public Task<List<Book>> GetAllReadOnlyAsync()
        => _context.Books.Include(p => p.Category).AsNoTracking().ToListAsync();

    public Task<Book?> GetByIdAsync(int id)
        => _context.Books.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync(); 
    }

    public async Task<List<Book>> GetFilteredBooksAsync(int? categoryId, decimal? minPrice, decimal? maxPrice)
    {
        var query = _context.Books
            .Include(b => b.Category) 
            .AsNoTracking();

        if (categoryId.HasValue)
        {
            query = query.Where(b => b.CategoryId == categoryId.Value);
        }

        if (minPrice.HasValue)
        {
            query = query.Where(b => b.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(b => b.Price <= maxPrice.Value);
        }

        return await query.ToListAsync();
    }

    public Task SaveChangesAsync()
        => _context.SaveChangesAsync();
}