namespace TicketShoppingCartMvcUI.Models.DTOs
{
    public class StockDisplayModel
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int Quantity { get; set; }
        public string? TicketName { get; set; }
    }
}
