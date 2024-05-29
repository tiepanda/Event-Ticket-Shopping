using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TicketShoppingCartMvcUI.Models.DTOs;
public class TicketDTO
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
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public double Price { get; set; }
    public string? Image { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public IFormFile? ImageFile { get; set; }
    public IEnumerable<SelectListItem>? CategoryList { get; set; }
}
