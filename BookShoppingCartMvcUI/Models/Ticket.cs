using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketShoppingCartMvcUI.Models
{
    [Table("Ticket")]
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? TicketName { get; set; }

        [Required]
        [MaxLength(40)]
        public string? PublisherName { get; set; }

        [Required]
        public string? Discription { get; set; } 
        [Required]
        public string? Location { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Price { get; set; }
        public string? Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }
        public Stock Stock { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }
        [NotMapped]
        public int Quantity { get; set; }


    }
}
