using System.ComponentModel.DataAnnotations;
namespace AspNetWeek2.Mvc.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Mã sách không được để trống.")]
    [MaxLength(20, ErrorMessage = "Mã sách không được vượt quá 20 ký tự.")]
    public string BookCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int MinStock { get; set; }

    public int CategoryId { get; set; }

    public BookCategory? Category { get; set; }
}