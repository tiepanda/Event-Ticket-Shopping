using Microsoft.EntityFrameworkCore;

namespace TicketShoppingCartMvcUI.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetStockByTicketId(int ticketId) => await _context.Stocks.FirstOrDefaultAsync(s => s.TicketId == ticketId);

        public async Task ManageStock(StockDTO stockToManage)
        {
            // if there is no stock for given ticket id, then add new record
            // if there is already stock for given ticket id, update stock's quantity
            var existingStock = await GetStockByTicketId(stockToManage.TicketId);
            if (existingStock is null)
            {
                var stock = new Stock { TicketId = stockToManage.TicketId, Quantity = stockToManage.Quantity };
                _context.Stocks.Add(stock);
            }
            else
            {
                existingStock.Quantity = stockToManage.Quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "")
        {
            var stocks = await (from ticket in _context.Tickets
                                join stock in _context.Stocks
                                on ticket.Id equals stock.TicketId
                                into ticket_stock
                                from ticketStock in ticket_stock.DefaultIfEmpty()
                                where string.IsNullOrWhiteSpace(sTerm) || ticket.TicketName.ToLower().Contains(sTerm.ToLower())
                                select new StockDisplayModel
                                {
                                    TicketId = ticket.Id,
                                    TicketName = ticket.TicketName,
                                    Quantity = ticketStock == null ? 0 : ticketStock.Quantity
                                }
                                ).ToListAsync();
            return stocks;
        }

    }

    public interface IStockRepository
    {
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "");
        Task<Stock?> GetStockByTicketId(int ticketId);
        Task ManageStock(StockDTO stockToManage);
    }
}
