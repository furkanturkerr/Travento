using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.ContactForm;
using Project3Travelin.Services.ContactService;

namespace Project3Travelin.Areas.Admin.Controllers;
[Area("Admin")]
public class ContactController : Controller
{
    private readonly IContactService _contactService;
    private readonly IMapper _mapper;

    public ContactController(IContactService contactService, IMapper mapper)
    {
        _contactService = contactService;
        _mapper = mapper;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var values = await _contactService.GetAllContactsAsync();
        return View(values);
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var value = await _contactService.GetContactByIdAsync(id);
        var details = _mapper.Map<UpdateContactDto>(value);
        return View(details);
    }
    
    [HttpPost]
    public async Task<IActionResult> Details(UpdateContactDto updateContactDto)
    {
        await _contactService.UpdateContactAsync(updateContactDto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(string id)
    {
        await _contactService.DeleteContactAsync(id);
        return RedirectToAction("Index");  
    }
}