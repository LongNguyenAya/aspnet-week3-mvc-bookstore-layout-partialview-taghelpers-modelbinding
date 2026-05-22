using AspNetWeek2.Mvc.Models;
using AspNetWeek2.Mvc.ViewModels;

namespace AspNetWeek2.Mvc.Services;

public class BookService
{
    private readonly List<Book> _Books = new()
    {
        new Book
        {
            Id = 1,
            Name = "Toán lớp 1",
            Category = "Giáo dục",
            Author = "Kim Đồng",
            Price = 20000,
            Quantity = 20,
            MinStock = 3,
            CreatedAt = new DateTime(2025, 5, 9, 9, 12, 0),
            UpdatedAt = DateTime.Now
        },
        new Book
        {
            Id = 2,
            Name = "Ngữ văn lớp 10",
            Category = "Giáo dục",
            Author = "Kim Đồng",
            Price = 35000,
            Quantity = 4,
            MinStock = 5,
            CreatedAt = new DateTime(2025, 5, 9, 9, 12, 0),
            UpdatedAt = DateTime.Now
        },
        new Book
        {
            Id = 3,
            Name = "Sống mòn",
            Category = "Tiểu thuyết",
            Author = "Nam Cao",
            Price = 98000,
            Quantity = 5,
            MinStock = 3,
            CreatedAt = new DateTime(2025, 5, 9, 9, 12, 0),
            UpdatedAt = DateTime.Now
        },
        new Book
        {
            Id = 4,
            Name = "Bạch tuyết và bảy chú lùn",
            Category = "Truyện tranh",
            Author = "Anh em nhà Grimm",
            Price = 20000,
            Quantity = 9,
            MinStock = 4,
            CreatedAt = new DateTime(2025, 5, 9, 9, 12, 0),
            UpdatedAt = DateTime.Now
        },
        new Book
        {
            Id = 5,
            Name = "Cho tôi xin một vé đi tuổi thơ",
            Category = "Tiểu thuyết",
            Author = "Nguyễn Nhật Ánh",
            Price = 100000,
            Quantity = 2,
            MinStock = 6,
            CreatedAt = new DateTime(2025, 5, 9, 9, 12, 0),
            UpdatedAt = DateTime.Now
        },
        new Book
        {
            Id = 6,
            Name = "Con cáo và chùm nho",
            Category = "Truyện tranh",
            Author = "Aesop",
            Price = 10000,
            Quantity = 7,
            MinStock = 3,
            CreatedAt = new DateTime(2025, 5, 9, 9, 12, 0),
            UpdatedAt = DateTime.Now
        }
    };

    public List<Book> GetAll()
    {
        return _Books;
    }

    public Book? GetById(int id)
    {
        return _Books.FirstOrDefault(Book => Book.Id == id);
    }

    public BookStatsViewModel GetStats()
    {
        var totalBooks = _Books.Count;

        var totalQuantity = _Books.Sum(Book => Book.Quantity);

        var totalInventoryValue = _Books.Sum(Book =>
            Book.Price * Book.Quantity);

        var outOfStockCount = _Books.Count(Book =>
            Book.Quantity <= 0);

        var needReorderCount = _Books.Count(Book =>
            Book.Quantity > 0 && Book.Quantity <= Book.MinStock);

        return new BookStatsViewModel
        {
            TotalBooks = totalBooks,
            TotalQuantity = totalQuantity,
            TotalInventoryValue = totalInventoryValue,
            OutOfStockCount = outOfStockCount,
            NeedReorderCount = needReorderCount
        };
    }
}