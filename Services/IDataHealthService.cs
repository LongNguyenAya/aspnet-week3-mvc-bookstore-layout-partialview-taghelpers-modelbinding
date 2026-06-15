namespace AspNetWeek2.Mvc.Services;

public interface IDataHealthService
{
    Task<Dictionary<string, string>> GetHealthReportAsync();
}