using AspNetWeek2.Mvc.Models;
using AspNetWeek2.Mvc.Services;
using AspNetWeek2.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetWeek2.Mvc.Controllers;

public class BooksController : Controller
{
    private readonly BookService _BookService;

    public BooksController(BookService BookService)
    {
        _BookService = BookService;
    }

    public IActionResult Index()
    {
        var Books = _BookService.GetAll()
            .Select(ToListItemViewModel)
            .ToList();

        return View(Books);
    }

    public IActionResult Detail(int id)
    {
        var Book = _BookService.GetById(id);

        if (Book == null)
        {
            return NotFound($"Không tìm thấy sách có id = {id}");
        }

        var viewModel = ToDetailViewModel(Book);

        return View(viewModel);
    }

    public IActionResult Stats()
    {
        var stats = _BookService.GetStats();

        return View(stats);
    }

    public IActionResult Welcome()
    {
        return Content("Welcome to ASP.NET Core MVC Lab02");
    }

    public IActionResult BookJson()
    {
        var Books = _BookService.GetAll()
            .Select(Book => new
            {
                Book.Id,
                Book.Name,
                Book.Category,
                Book.Author,
                Book.Price,
                Book.Quantity
            });

        return Json(Books);
    }

    public IActionResult GoToList()
    {
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Force404()
    {
        return NotFound("Đây là response 404 demo từ action Force404.");
    }

    private static BookListItemViewModel ToListItemViewModel(Book Book)
    {
        return new BookListItemViewModel
        {
            Id = Book.Id,
            Name = Book.Name,
            Category = Book.Category,
            Price = Book.Price,
            Quantity = Book.Quantity,
            MinStock = Book.MinStock
        };
    }

    private static BookDetailViewModel ToDetailViewModel(Book Book)
    {
        return new BookDetailViewModel
        {
            Id = Book.Id,
            Name = Book.Name,
            Category = Book.Category,
            Author = Book.Author,
            Price = Book.Price,
            Quantity = Book.Quantity,
            MinStock = Book.MinStock,
            CreatedAt = Book.CreatedAt,
            UpdatedAt = Book.UpdatedAt
        };
    }

    [HttpGet]
    public IActionResult Search(string? keyword, decimal? minPrice)
    {
        var Books = _BookService.Search(keyword, minPrice)
            .Select(ToListItemViewModel)
            .ToList();

        var viewModel = new BookSearchViewModel
        {
            Keyword = keyword ?? "",
            MinPrice = minPrice,
            Books = Books
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new BookCreateViewModel
        {
            Quantity = 1,
            MinStock = 1
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _BookService.Create(model);

        TempData["SuccessMessage"] = "Đã thêm sách thành công.";

        return RedirectToAction(nameof(Index));
    }
}