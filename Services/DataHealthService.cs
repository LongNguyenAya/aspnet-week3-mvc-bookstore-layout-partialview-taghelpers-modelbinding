using Microsoft.EntityFrameworkCore;
using AspNetWeek2.Mvc.Data;
using AspNetWeek2.Mvc.Models;
using AspNetWeek2.Mvc.ViewModels;

namespace AspNetWeek2.Mvc.Services;

public class DataHealthService : IDataHealthService
{
    private readonly AppDbContext _context;
    private readonly IOrderService _orderService;

    public DataHealthService(AppDbContext context, IOrderService orderService)
    {
        _context = context;
        _orderService = orderService;
    }

    public async Task<Dictionary<string, string>> GetHealthReportAsync()
    {
        var healthReport = new Dictionary<string, string>();

        var canConnect = await _context.Database.CanConnectAsync();
        healthReport.Add("Database Connection", canConnect ? "Thành công (Connected)" : "Thất bại");

        var categoryCount = await _context.BookCategories.CountAsync();
        var bookCount = await _context.Books.CountAsync();
        healthReport.Add("Seed Data Status", (categoryCount > 0 && bookCount > 0) 
            ? $"Đã có dữ liệu ({categoryCount} Thể loại, {bookCount} Sách)" 
            : "Chưa có dữ liệu mẫu");

        var readOnlyBooks = await _context.Books.AsNoTracking().Take(1).ToListAsync();
        var isTracked = _context.ChangeTracker.Entries<Book>().Any();
        healthReport.Add("No-Tracking Test", !isTracked 
            ? "Hoạt động đúng (Dữ liệu không bị EF theo dõi khi dùng AsNoTracking)" 
            : "Sai cấu hình (Vẫn bị theo dõi)");

        healthReport.Add("Transaction & Rollback Test", await TestTransactionRollbackAsync());

        return healthReport;
    }

    private async Task<string> TestTransactionRollbackAsync()
    {
        var sampleBook = await _context.Books.FirstOrDefaultAsync();
        if (sampleBook == null) return "Thất bại (Không có sách để test)";

        var initialQuantity = sampleBook.Quantity;

        var invalidOrderModel = new OrderCreateViewModel
        {
            BookId = sampleBook.Id,
            Quantity = initialQuantity + 999
        };

        try
        {
            await _orderService.CreateOrderAsync(invalidOrderModel);
            return "Thất bại (Đơn hàng sai nhưng vẫn tạo thành công)";
        }
        catch (Exception ex)
        {
            _context.Entry(sampleBook).State = EntityState.Detached;
            var currentBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == sampleBook.Id);
            
            if (currentBook != null && currentBook.Quantity == initialQuantity)
            {
                return $"Hoạt động đúng. Hệ thống đã chặn và Rollback thành công! (Lỗi bắt được: '{ex.Message}', Số lượng kho giữ nguyên: {currentBook.Quantity})";
            }
            
            return "Thất bại (Văng lỗi nhưng kho vẫn bị trừ)";
        }
    }
}