using AspNetWeek2.Mvc.Models;
using AspNetWeek2.Mvc.ViewModels;
using AspNetWeek2.Mvc.Repositories;
using Microsoft.Extensions.Options;

namespace AspNetWeek2.Mvc.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly AppSettings _settings;

    public BookService(IBookRepository bookRepository, IOptions<AppSettings> options)
    {
        _bookRepository = bookRepository;
        _settings = options.Value;
    }

    public async Task<List<BookListItemViewModel>> GetBookListAsync()
    {
        var books = await _bookRepository.GetAllReadOnlyAsync();
        return books.Select(p => new BookListItemViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Quantity = p.Quantity,
            MinStock = p.MinStock,
            Category = p.Category != null ? p.Category.Name : "N/A"
        }).ToList();
    }

    public async Task<BookDetailViewModel?> GetBookDetailAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null) return null;

        return new BookDetailViewModel
        {
            Id = book.Id,
            Name = book.Name,
            Author = book.Author,
            Price = book.Price,
            Quantity = book.Quantity,
            MinStock = _settings.LowStockThreshold, 
            Category = book.Category != null ? book.Category.Name : "N/A",
            CreatedAt = DateTime.Now, 
            UpdatedAt = DateTime.Now
        };
    }

    public async Task<List<BookListItemViewModel>> GetFilteredBooksAsync(int? categoryId, decimal? minPrice, decimal? maxPrice)
    {
        var books = await _bookRepository.GetFilteredBooksAsync(categoryId, minPrice, maxPrice);

        return books.Select(b => new BookListItemViewModel
        {
            Id = b.Id,
            Name = b.Name,
            Price = b.Price,
            Quantity = b.Quantity,
            Category = b.Category != null ? b.Category.Name : "N/A"
        }).ToList();
    }
}