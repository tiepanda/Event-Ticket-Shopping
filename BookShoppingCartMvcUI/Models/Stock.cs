using System.ComponentModel.DataAnnotations.Schema;

namespace TicketShoppingCartMvcUI.Models
{
    [Table("Stock")]
    public class Stock
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int Quantity { get; set; }

        public Ticket? Ticket { get; set; }
    }
}
