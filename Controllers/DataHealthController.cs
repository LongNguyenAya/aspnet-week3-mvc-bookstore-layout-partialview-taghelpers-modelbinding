using Microsoft.AspNetCore.Mvc;
using AspNetWeek2.Mvc.Services;

namespace AspNetWeek2.Mvc.Controllers;

public class DataHealthController : Controller
{
    private readonly IDataHealthService _dataHealthService;

    public DataHealthController(IDataHealthService dataHealthService)
    {
        _dataHealthService = dataHealthService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var report = await _dataHealthService.GetHealthReportAsync();
        return View(report);
    }
}