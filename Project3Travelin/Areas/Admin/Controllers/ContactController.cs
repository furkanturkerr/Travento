using System.Net.Http.Headers;
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
    private readonly IConfiguration _configuration;

    public ContactController(IContactService contactService, IMapper mapper, IConfiguration configuration)
    {
        _contactService = contactService;
        _mapper = mapper;
        _configuration = configuration;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var values = await _contactService.GetAllContactsAsync();
        return View(values);
    }

    public async Task<IActionResult> IsStatus(string id)
    {
        await _contactService.IsStatus(id);
        return RedirectToAction("Index");
    }
    
   [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var value = await _contactService.GetContactByIdAsync(id);
        var details = _mapper.Map<UpdateContactDto>(value);

        if (!details.IsStatus)
            await _contactService.IsStatus(id);

        var prompt = details.comments;
        
        var apiKey = _configuration["MailSettings:ApiKey"];
        
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        
        var requestData = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new
                {
                    role = "system",
                    content = "Sen bir tur ve seyahat acentesi için çalışan bir yapay zeka asistanısın. Kullanıcıların seyahat, tur, rezervasyon ve destinasyonlarla ilgili sorularına detaylı, samimi ve heyecan verici cevaplar veriyorsun. Amacın kullanıcıların hayalindeki tatili bulmalarına yardımcı olmak, onları doğru tura yönlendirmek ve rezervasyon süreçlerinde rehberlik etmek. Cevaplarında her zaman olumlu, yardımsever ve motive edici bir dil kullan. Kullanıcı bir destinasyon sorduğunda o yerin güzelliklerini ve öne çıkan özelliklerini vurgula. Fiyat, tarih veya tur detayları sorulduğunda elimdeki bilgileri net ve anlaşılır şekilde aktar. Her zaman müşteri memnuniyetini ön planda tut."
                },
                new { role = "user", content = prompt }
            },
            temperature = 0.5
        };

        var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
            var content = result.choices[0].message.content;
            ViewBag.ancwerAI = content;
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ViewBag.ancwerAI = $"Bir hata oluştu: {response.StatusCode}<br/>Detay: {errorMessage}";
        }
        
        return View(details); 
    }

    public class OpenAIResponse
    {
        public List<Choice> choices { get; set; }
    }

    public class Choice
    {
        public Message message { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
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