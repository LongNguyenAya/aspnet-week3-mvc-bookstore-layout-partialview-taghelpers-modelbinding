namespace AspNetWeek2.Mvc.Repositories;
using AspNetWeek2.Mvc.Models;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
    Task<List<Book>> GetAllReadOnlyAsync();
    Task<Book?> GetByIdAsync(int id);
    Task AddAsync(Book book);
    Task SaveChangesAsync();
    Task<List<Book>> GetFilteredBooksAsync(int? categoryId, decimal? minPrice, decimal? maxPrice);
}