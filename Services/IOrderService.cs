using AspNetWeek2.Mvc.Models; 
namespace AspNetWeek2.Mvc.Services;
using AspNetWeek2.Mvc.ViewModels;

public interface IOrderService
{
    Task CreateOrderAsync(OrderCreateViewModel model);
}