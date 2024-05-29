using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace TicketShoppingCartMvcUI.Repositories;

[Authorize(Roles = nameof(Roles.Admin))]
public class ReportRepository : IReportRepository
{
    private readonly ApplicationDbContext _context;
    public ReportRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TopNSoldTicketModel>> GetTopNSellingTicketsByDate(DateTime startDate, DateTime endDate)
    {
        var startDateParam = new SqlParameter("@startDate", startDate);
        var endDateParam = new SqlParameter("@endDate", endDate);
        var topFiveSoldTickets = await _context.Database.SqlQueryRaw<TopNSoldTicketModel>("exec Usp_GetTopNSellingTicketsByDate @startDate,@endDate", startDateParam, endDateParam).ToListAsync();
        return topFiveSoldTickets;
    }

}

public interface IReportRepository
{
    Task<IEnumerable<TopNSoldTicketModel>> GetTopNSellingTicketsByDate(DateTime startDate, DateTime endDate);
}