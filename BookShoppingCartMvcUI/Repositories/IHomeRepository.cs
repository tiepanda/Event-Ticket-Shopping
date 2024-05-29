namespace TicketShoppingCartMvcUI
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Ticket>> GetTickets(string sTerm = "", int categoryId = 0);
        Task<IEnumerable<Category>> Categorys();
    }
}