using TicketShoppingCartMvcUI.Models;
using TicketShoppingCartMvcUI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TicketShoppingCartMvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm="",int categoryId=0)
        {
            IEnumerable<Ticket> tickets = await _homeRepository.GetTickets(sterm, categoryId);
            IEnumerable<Category> categorys = await _homeRepository.Categorys();
            TicketDisplayModel ticketModel = new TicketDisplayModel
            {
              Tickets=tickets,
              Categorys=categorys,
              STerm=sterm,
              CategoryId=categoryId
            };
            return View(ticketModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}