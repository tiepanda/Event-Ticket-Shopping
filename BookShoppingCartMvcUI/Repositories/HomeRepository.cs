

using Microsoft.EntityFrameworkCore;

namespace TicketShoppingCartMvcUI.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> Categorys()
        {
            return await _db.Categorys.ToListAsync();
        }
        public async Task<IEnumerable<Ticket>> GetTickets(string sTerm = "", int categoryId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Ticket> tickets = await (from ticket in _db.Tickets
                         join category in _db.Categorys
                         on ticket.CategoryId equals category.Id
                         join stock in _db.Stocks
                         on ticket.Id equals stock.TicketId
                         into ticket_stocks
                         from ticketWithStock in ticket_stocks.DefaultIfEmpty()
                         where string.IsNullOrWhiteSpace(sTerm) || (ticket != null && ticket.TicketName.ToLower().StartsWith(sTerm))
                         select new Ticket
                         {
                             Id = ticket.Id,
                             Image = ticket.Image,
                             PublisherName = ticket.PublisherName,
                             TicketName = ticket.TicketName,
                             CategoryId = ticket.CategoryId,
                             Price = ticket.Price,
                             CategoryName = category.CategoryName,
                             Discription =ticket.Discription,
                             Date = ticket.Date,
                             Quantity=ticketWithStock==null? 0:ticketWithStock.Quantity
                         }
                         ).ToListAsync();
            if (categoryId > 0)
            {

                tickets = tickets.Where(a => a.CategoryId == categoryId).ToList();
            }
            return tickets;

        }
    }
}
