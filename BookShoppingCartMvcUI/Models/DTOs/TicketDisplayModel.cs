namespace TicketShoppingCartMvcUI.Models.DTOs
{
    public class TicketDisplayModel
    {
        public IEnumerable<Ticket> Tickets { get; set; }
        public IEnumerable<Category> Categorys { get; set; }
        public string STerm { get; set; } = "";
        public int CategoryId { get; set; } = 0;
    }
}
