using Microsoft.Extensions.Options;
using AspNetWeek2.Mvc.ViewModels;

namespace AspNetWeek2.Mvc.Services; 

public interface IBookService
{
    Task<List<BookListItemViewModel>> GetBookListAsync();
    
    Task<BookDetailViewModel?> GetBookDetailAsync(int id);
    Task<List<BookListItemViewModel>> GetFilteredBooksAsync(int? categoryId, decimal? minPrice, decimal? maxPrice);
}