using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Project3Travelin.Dtos.BookingDtos;
using Project3Travelin.Dtos.MailKitDtos;
using Project3Travelin.Services.BookingServices;

namespace Project3Travelin.Areas.Admin.Controllers;
[Area("Admin")]
public class BookingController : Controller
{
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public BookingController(IBookingService bookingService, IMapper mapper, IConfiguration configuration)
    {
        _bookingService = bookingService;
        _mapper = mapper;
        _configuration = configuration;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var values = await _bookingService.GetAllBookingAsync();
        return View(values);
    }

    public async Task<IActionResult> Delete(string id)
    {
        await _bookingService.DeleteBookingAsync(id);
        return RedirectToAction("Index");  
    }

    public async Task<IActionResult> Approve(string id)
    {
        await _bookingService.Approve(id);

        var value = await _bookingService.GetBookingByIdAsync(id);
        if (value == null)
            return RedirectToAction("Index");

        try
        {
            var email = _configuration["MailSettings:Email"];
            var password = _configuration["MailSettings:Password"];
            var companyName = _configuration["MailSettings:CompanyName"];

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(companyName, email));
            mimeMessage.To.Add(new MailboxAddress($"{value.FirstName} {value.LastName}", value.Email));
            mimeMessage.Subject = "Travento - Rezervasyon Onayı";

            var bodyBuilder = new BodyBuilder
            {
                TextBody = $@"Sayın {value.FirstName} {value.LastName},

Rezervasyonunuz başarıyla onaylanmıştır.

Tur: {value.TourTitle}
Tarih: {value.BookingDate:dd.MM.yyyy}

Bizi tercih ettiğiniz için teşekkür ederiz.
İyi yolculuklar dileriz."
            };

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(email, password);
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);

            TempData["SuccessMessage"] = "Rezervasyon onaylandı ve bilgilendirme maili gönderildi.";
        }
        catch
        {
            TempData["ErrorMessage"] = "Rezervasyon onaylandı fakat mail gönderilemedi.";
        }

        return RedirectToAction("Index");
    }
}