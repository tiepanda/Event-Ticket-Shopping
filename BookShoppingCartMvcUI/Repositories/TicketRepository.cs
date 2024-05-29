using Microsoft.EntityFrameworkCore;

namespace TicketShoppingCartMvcUI.Repositories
{
    public interface ITicketRepository
    {
        Task AddTicket(Ticket ticket);
        Task DeleteTicket(Ticket ticket);
        Task<Ticket?> GetTicketById(int id);
        Task<IEnumerable<Ticket>> GetTickets();
        Task UpdateTicket(Ticket ticket);
    }

    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;
        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicket(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<Ticket?> GetTicketById(int id) => await _context.Tickets.FindAsync(id);

        public async Task<IEnumerable<Ticket>> GetTickets() => await _context.Tickets.Include(a=>a.Category).ToListAsync();
    }
}
