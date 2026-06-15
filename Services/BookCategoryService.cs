using Microsoft.EntityFrameworkCore;
using AspNetWeek2.Mvc.Data;
using AspNetWeek2.Mvc.Models;

namespace AspNetWeek2.Mvc.Services;

public class BookCategoryService : IBookCategoryService
{
    private readonly AppDbContext _context;

    public BookCategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BookCategoryReportViewModel>> GetCategoryReportAsync()
    {
        return await _context.BookCategories
            .Select(c => new BookCategoryReportViewModel
            {
                CategoryName = c.Name,
                BookCount = _context.Books.Count(b => b.CategoryId == c.Id)
            })
            .AsNoTracking()
            .ToListAsync();
    }
}