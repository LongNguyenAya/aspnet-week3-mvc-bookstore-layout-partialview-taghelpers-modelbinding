using AspNetWeek2.Mvc.Models;
using Microsoft.EntityFrameworkCore;
using AspNetWeek2.Mvc.Data;
using AspNetWeek2.Mvc.ViewModels;

namespace AspNetWeek2.Mvc.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateOrderAsync(OrderCreateViewModel model)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == model.BookId);
            if (book == null) throw new Exception("Không tìm thấy sách này");
            
            if (book.Quantity < model.Quantity) throw new Exception("Số lượng hàng trong kho không đủ");

            var order = new Order
            {
                CreatedAt = DateTime.Now,
                TotalAmount = book.Price * model.Quantity
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); 

            var item = new OrderItem
            {
                OrderId = order.Id,
                BookId = book.Id,
                Quantity = model.Quantity,
                Price = book.Price 
            };
            _context.OrderItems.Add(item);

            book.Quantity -= model.Quantity; 

            await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}