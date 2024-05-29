using TicketShoppingCartMvcUI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TicketShoppingCartMvcUI.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]
public class TicketController : Controller
{
    private readonly ITicketRepository _ticketRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IFileService _fileService;

    public TicketController(ITicketRepository ticketRepo, ICategoryRepository categoryRepo, IFileService fileService)
    {
        _ticketRepo = ticketRepo;
        _categoryRepo = categoryRepo;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index()
    {
        var tickets = await _ticketRepo.GetTickets();
        return View(tickets);
    }

    public async Task<IActionResult> AddTicket()
    {
        var categorySelectList = (await _categoryRepo.GetCategorys()).Select(category => new SelectListItem
        {
            Text = category.CategoryName,
            Value = category.Id.ToString(),
        });
        TicketDTO ticketToAdd = new() { CategoryList = categorySelectList };
        return View(ticketToAdd);
    }

    [HttpPost]
    public async Task<IActionResult> AddTicket(TicketDTO ticketToAdd)
    {
        var categorySelectList = (await _categoryRepo.GetCategorys()).Select(category => new SelectListItem
        {
            Text = category.CategoryName,
            Value = category.Id.ToString(),
        });
        ticketToAdd.CategoryList = categorySelectList;

        if (!ModelState.IsValid)
            return View(ticketToAdd);

        try
        {
            if (ticketToAdd.ImageFile != null)
            {
                if(ticketToAdd.ImageFile.Length> 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Image file can not exceed 1 MB");
                }
                string[] allowedExtensions = [".jpeg",".jpg",".png"];
                string imageName=await _fileService.SaveFile(ticketToAdd.ImageFile, allowedExtensions);
                ticketToAdd.Image = imageName;
            }
            // manual mapping of TicketDTO -> Ticket
            Ticket ticket = new()
            {
                Id = ticketToAdd.Id,
                TicketName = ticketToAdd.TicketName,
                PublisherName = ticketToAdd.PublisherName,
                Image = ticketToAdd.Image,
                CategoryId = ticketToAdd.CategoryId,
                Price = ticketToAdd.Price,
                Discription = ticketToAdd.Discription,
                Location = ticketToAdd.Location,
                Date = ticketToAdd.Date
            };
            await _ticketRepo.AddTicket(ticket);
            TempData["successMessage"] = "Ticket is added successfully";
            return RedirectToAction(nameof(AddTicket));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"]= ex.Message;
            return View(ticketToAdd);
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(ticketToAdd);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on saving data";
            return View(ticketToAdd);
        }
    }

    public async Task<IActionResult> UpdateTicket(int id)
    {
        var ticket = await _ticketRepo.GetTicketById(id);
        if(ticket==null)
        {
            TempData["errorMessage"] = $"Ticket with the id: {id} does not found";
            return RedirectToAction(nameof(Index));
        }
        var categorySelectList = (await _categoryRepo.GetCategorys()).Select(category => new SelectListItem
        {
            Text = category.CategoryName,
            Value = category.Id.ToString(),
            Selected=category.Id==ticket.CategoryId
        });
        TicketDTO ticketToUpdate = new() 
        { 
            CategoryList = categorySelectList,
            TicketName=ticket.TicketName,
            PublisherName=ticket.PublisherName,
            CategoryId=ticket.CategoryId,
            Price=ticket.Price,
            Image=ticket.Image,
            Discription = ticket.Discription,
            Location = ticket.Location,
            Date = ticket.Date
        };
        return View(ticketToUpdate);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTicket(TicketDTO ticketToUpdate)
    {
        var categorySelectList = (await _categoryRepo.GetCategorys()).Select(category => new SelectListItem
        {
            Text = category.CategoryName,
            Value = category.Id.ToString(),
            Selected=category.Id==ticketToUpdate.CategoryId
        });
        ticketToUpdate.CategoryList = categorySelectList;

        if (!ModelState.IsValid)
            return View(ticketToUpdate);

        try
        {
            string oldImage = "";
            if (ticketToUpdate.ImageFile != null)
            {
                if (ticketToUpdate.ImageFile.Length > 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Image file can not exceed 1 MB");
                }
                string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                string imageName = await _fileService.SaveFile(ticketToUpdate.ImageFile, allowedExtensions);
                // hold the old image name. Because we will delete this image after updating the new
                oldImage = ticketToUpdate.Image;
                ticketToUpdate.Image = imageName;
            }
            // manual mapping of TicketDTO -> Ticket
            Ticket ticket = new()
            {
                Id=ticketToUpdate.Id,
                TicketName = ticketToUpdate.TicketName,
                PublisherName = ticketToUpdate.PublisherName,
                CategoryId = ticketToUpdate.CategoryId,
                Price = ticketToUpdate.Price,
                Image = ticketToUpdate.Image,
                Discription = ticketToUpdate.Discription,
                Location = ticketToUpdate.Location,
                Date = ticketToUpdate.Date
            };
            await _ticketRepo.UpdateTicket(ticket);
            // if image is updated, then delete it from the folder too
            if(!string.IsNullOrWhiteSpace(oldImage))
            {
                _fileService.DeleteFile(oldImage);
            }
            TempData["successMessage"] = "Ticket is updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(ticketToUpdate);
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(ticketToUpdate);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on saving data";
            return View(ticketToUpdate);
        }
    }

    public async Task<IActionResult> DeleteTicket(int id)
    {
        try
        {
            var ticket = await _ticketRepo.GetTicketById(id);
            if (ticket == null)
            {
                TempData["errorMessage"] = $"Ticket with the id: {id} does not found";
            }
            else
            {
                await _ticketRepo.DeleteTicket(ticket);
                if (!string.IsNullOrWhiteSpace(ticket.Image))
                {
                    _fileService.DeleteFile(ticket.Image);
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on deleting the data";
        }
        return RedirectToAction(nameof(Index));
    }

}
