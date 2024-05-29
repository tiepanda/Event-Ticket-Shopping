namespace TicketShoppingCartMvcUI.Models.DTOs;

public record TopNSoldTicketModel(string TicketName, string PublisherName, int TotalUnitSold);
public record TopNSoldTicketsVm(DateTime StartDate, DateTime EndDate, IEnumerable<TopNSoldTicketModel> TopNSoldTickets);