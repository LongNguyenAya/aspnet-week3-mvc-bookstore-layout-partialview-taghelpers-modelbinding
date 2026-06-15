using AspNetWeek2.Mvc.Models;
using AspNetWeek2.Mvc.Services;
using AspNetWeek2.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetWeek2.Mvc.Controllers;

public class BooksController : Controller
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetBookListAsync();
        return View(books);
    }
    
    [Route("Books/Detail/{id:int}")] 
    public async Task<IActionResult> Detail(int id)
    {
        var bookDetail = await _bookService.GetBookDetailAsync(id);
        
        if (bookDetail == null)
        {
            return NotFound(); 
        }
        
        return View(bookDetail);
    }

    [HttpGet]
    [Route("Books/Filter")]
    public async Task<IActionResult> Filter(int? categoryId, decimal? minPrice, decimal? maxPrice)
    {
        var filteredResult = await _bookService.GetFilteredBooksAsync(categoryId, minPrice, maxPrice);
        
        return View("Index", filteredResult);
    }
}