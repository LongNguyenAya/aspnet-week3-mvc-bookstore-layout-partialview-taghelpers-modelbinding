using Microsoft.AspNetCore.Mvc;
using AspNetWeek2.Mvc.Services;

namespace AspNetWeek2.Mvc.Controllers;

public class BookCategoriesController : Controller
{
    private readonly IBookCategoryService _categoryService;

    public BookCategoriesController(IBookCategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [Route("Categories")]
    public async Task<IActionResult> Index()
    {
        var report = await _categoryService.GetCategoryReportAsync();
        return View(report);
    }
}