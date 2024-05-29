using System.ComponentModel.DataAnnotations;

namespace TicketShoppingCartMvcUI.Models.DTOs
{
    public class StockDTO
    {
        public int TicketId { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int Quantity { get; set; }
    }
}
