using Microsoft.AspNetCore.Mvc;

namespace TicketShoppingCartMvcUI.Controllers;
public class ReportsController : Controller
{
    private readonly IReportRepository _reportRepository;
    public ReportsController(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }
    // GET: ReportsController
    public async Task<ActionResult> TopFiveSellingTickets(DateTime? sDate = null, DateTime? eDate = null)
    {
            // by default, get last 7 days record
            DateTime startDate = sDate ?? DateTime.UtcNow.AddDays(-7);
            DateTime endDate = eDate ?? DateTime.UtcNow;
            var topFiveSellingTickets = await _reportRepository.GetTopNSellingTicketsByDate(startDate, endDate);
            var vm = new TopNSoldTicketsVm(startDate, endDate, topFiveSellingTickets);
            return View(vm);
        //try
        //{

        //}
        
        //catch (Exception ex)
        //{
        //    TempData["errorMessage"] = "Something went wrong";
        //    return RedirectToAction("Index", "Home");
        //}
    }
}