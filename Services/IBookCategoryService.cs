using AspNetWeek2.Mvc.Models;

namespace AspNetWeek2.Mvc.Services;

public interface IBookCategoryService
{
    Task<List<BookCategoryReportViewModel>> GetCategoryReportAsync();
}