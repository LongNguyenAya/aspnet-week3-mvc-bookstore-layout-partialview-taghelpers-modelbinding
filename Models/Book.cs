namespace AspNetWeek2.Mvc.Models;

public class Book
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Category { get; set; } = "";

    public string Author { get; set; } = "";

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int MinStock { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
