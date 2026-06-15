using System.ComponentModel.DataAnnotations;

namespace AspNetWeek2.Mvc.ViewModels; 

public class OrderCreateViewModel
{
    [Required(ErrorMessage = "Vui lòng chọn một cuốn sách.")]
    public int BookId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số lượng.")]
    [Range(1, int.MaxValue, ErrorMessage = "Số lượng đặt mua phải lớn hơn hoặc bằng 1.")]
    public int Quantity { get; set; }
}