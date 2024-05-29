using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketShoppingCartMvcUI.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        [Required]
        public int TicketId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }   
        public Ticket Ticket { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
