using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.ContactForm;
using Project3Travelin.Services.ContactService;

namespace Project3Travelin.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }


    public IActionResult Iletisim()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ContactForm(CreateContactDto createContact)
    {
        createContact.CreateDate = DateTime.Now;
        createContact.IsStatus = false;
        await _contactService.CreateContactAsync(createContact);
        return RedirectToAction("Iletisim", "Contact");
    }
}