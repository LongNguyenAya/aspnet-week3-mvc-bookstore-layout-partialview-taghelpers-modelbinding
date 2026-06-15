using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetWeek2.Mvc.Data;
using AspNetWeek2.Mvc.Models;
using AspNetWeek2.Mvc.Services;
using AspNetWeek2.Mvc.ViewModels;

namespace AspNetWeek2.Mvc.Controllers;

public class OrdersController : Controller
{
    private readonly AppDbContext _context;
    private readonly IOrderService _orderService;

    public OrdersController(AppDbContext context, IOrderService orderService)
    {
        _context = context;
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var books = await _context.Books.AsNoTracking().ToListAsync();
        ViewBag.Books = new SelectList(books, "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var books = await _context.Books.AsNoTracking().ToListAsync();
            ViewBag.Books = new SelectList(books, "Id", "Name");
            return View(model);
        }

        try
        {
            await _orderService.CreateOrderAsync(model);
            
            return RedirectToAction("Index", "Books");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Lỗi đặt hàng: {ex.Message}");
            
            var books = await _context.Books.AsNoTracking().ToListAsync();
            ViewBag.Books = new SelectList(books, "Id", "Name");
            return View(model);
        }
    }
}