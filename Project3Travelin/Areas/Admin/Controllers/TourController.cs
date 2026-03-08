using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.BookingDtos;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Services.BookingServices;
using Project3Travelin.Services.TourServices;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Project3Travelin.Areas.Admin.Controllers;
[Area("Admin")]
public class TourController : Controller
{
    private readonly ITourService _tourService;
    private readonly IMapper _mapper;
    private readonly IBookingService _bookingService;

    public TourController(ITourService tourService, IMapper mapper, IBookingService bookingService)
    {
        _tourService    = tourService;
        _mapper         = mapper;
        _bookingService = bookingService;
    }

    public async Task<IActionResult> TourList(int page = 1)
    {
        var value      = await _tourService.GetAllTourAsync();
        int pageSize   = 9;
        int totalCount = value.Count;
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        var paged      = value.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages  = totalPages;
        ViewBag.TotalCount  = totalCount;
        return View(paged);
    }

    public IActionResult CreateTour() => View();

    [HttpPost]
    public async Task<IActionResult> CreateTour(CreateTourDto createTourDto)
    {
        await _tourService.CreateTourAsync(createTourDto);
        return RedirectToAction("TourList");
    }

    public async Task<IActionResult> DeleteTour(string id)
    {
        await _tourService.DeleteTourAsync(id);
        return RedirectToAction("TourList");
    }

    [HttpGet]
    public async Task<IActionResult> EditTour(string id)
    {
        var value     = await _tourService.GetTourByIdAsync(id);
        var updateDto = _mapper.Map<UpdateTourDto>(value);
        return View(updateDto);
    }

    [HttpPost]
    public async Task<IActionResult> EditTour(UpdateTourDto updateTourDto)
    {
        await _tourService.UpdateTourAsync(updateTourDto);
        return RedirectToAction("TourList");
    }

    // PDF Rezervasyon Raporu
    public async Task<IActionResult> TourBookingReport(string id)
    {
        var tour = await _tourService.GetTourByIdAsync(id);
        if (tour == null) return NotFound();

        var allBookings = await _bookingService.GetAllBookingAsync();
        var bookings    = allBookings.Where(b => b.TourId == id).ToList();

        var confirmed   = bookings.Count(b => b.IsConfirmed);
        var pending     = bookings.Count(b => !b.IsConfirmed);
        var totalPeople = bookings.Sum(b => b.PersonCount);
        var now         = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

        var pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1.5f, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(10));

                // HEADER
                page.Header().Background("#0d9e8f").Padding(16).Column(col =>
                {
                    col.Item().Text("Rezervasyon Raporu")
                        .FontSize(20).Bold().FontColor("#ffffff");
                    col.Item().Text($"{tour.Title}  -  {tour.City}, {tour.Country}  -  {tour.DayNight}  |  {now}")
                        .FontSize(10).FontColor("#d0f0ec");
                });

                // CONTENT
                page.Content().PaddingTop(12).Column(col =>
                {
                    // İstatistik kutuları
                    col.Item().Row(row =>
                    {
                        StatBox(row, "Toplam",      bookings.Count.ToString(), "#0d9e8f");
                        StatBox(row, "Onaylandi",   confirmed.ToString(),      "#27ae60");
                        StatBox(row, "Bekleyen",    pending.ToString(),        "#f39c12");
                        StatBox(row, "Toplam Kisi", totalPeople.ToString(),    "#0d9e8f");
                        StatBox(row, "Kapasite",    tour.Capacity.ToString(),  "#6b7f86");
                    });

                    col.Item().PaddingTop(14);

                    // Bölüm başlığı
                    col.Item().Background("#f4f8f8").Padding(8)
                        .Text("REZERVASYON LISTESI")
                        .FontSize(9).Bold().FontColor("#1a2e35");

                    if (!bookings.Any())
                    {
                        col.Item().PaddingTop(20).AlignCenter()
                            .Text("Bu tura ait rezervasyon bulunamadi.")
                            .FontSize(12).FontColor("#aaaaaa");
                    }
                    else
                    {
                        col.Item().Table(table =>
                        {
                            // Kolon genişlikleri
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(22);    // #
                                cols.RelativeColumn(2);     // Ad Soyad
                                cols.RelativeColumn(2.2f);  // E-posta
                                cols.RelativeColumn(1.3f);  // Telefon
                                cols.ConstantColumn(28);    // Kişi
                                cols.RelativeColumn(1.2f);  // Tur Tarihi
                                cols.RelativeColumn(1.4f);  // Kayıt Tarihi
                                cols.ConstantColumn(52);    // Durum
                            });

                            // Başlık satırı — header lambda içinde Cell() kullanılır
                            table.Header(header =>
                            {
                                string[] hs = { "#", "Ad Soyad", "E-posta", "Telefon", "Kisi", "Tur Tarihi", "Kayit Tarihi", "Durum" };
                                foreach (var h in hs)
                                {
                                    header.Cell()
                                        .Background("#f4f8f8")
                                        .BorderBottom(1).BorderColor("#e0eaea")
                                        .Padding(6)
                                        .Text(h).FontSize(8).Bold().FontColor("#6b7f86");
                                }
                            });

                            // Veri satırları
                            int i = 1;
                            foreach (var b in bookings.OrderByDescending(x => x.CreatedAt))
                            {
                                var bg          = i % 2 == 0 ? "#fafcfc" : "#ffffff";
                                var statusText  = b.IsConfirmed ? "Onaylandi" : "Bekliyor";
                                var statusColor = b.IsConfirmed ? "#27ae60"   : "#f39c12";
                                var statusBg    = b.IsConfirmed ? "#eafaf1"   : "#fff8e6";

                                TCell(table, bg, i.ToString(),                             "#aaaaaa", 8);
                                TCell(table, bg, $"{b.FirstName} {b.LastName}",            "#1a2e35", 10, bold: true);
                                TCell(table, bg, b.Email    ?? "-",                        "#6b7f86");
                                TCell(table, bg, b.Phone    ?? "-",                        "#1a2e35");
                                TCell(table, bg, b.PersonCount.ToString(),                 "#0d9e8f", 10, bold: true, center: true);
                                TCell(table, bg, b.BookingDate.ToString("dd.MM.yyyy"),     "#1a2e35");
                                TCell(table, bg, b.CreatedAt.ToString("dd.MM.yyyy HH:mm"), "#6b7f86", 8);

                                // Durum hücresi
                                table.Cell()
                                    .Background(bg)
                                    .BorderBottom(1).BorderColor("#f0f0f0")
                                    .Padding(4).AlignMiddle().AlignCenter()
                                    .Background(statusBg).Padding(3)
                                    .Text(statusText).FontSize(8).Bold().FontColor(statusColor);

                                i++;
                            }
                        });
                    }
                });

                // FOOTER
                page.Footer()
                    .BorderTop(1).BorderColor("#e0eaea")
                    .PaddingTop(6)
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .Text($"Travelin Admin  -  {tour.Title}")
                            .FontSize(8).FontColor("#6b7f86");
                        row.ConstantItem(160).AlignRight().Text(txt =>
                        {
                            txt.Span("Sayfa ").FontSize(8).FontColor("#6b7f86");
                            txt.CurrentPageNumber().FontSize(8).FontColor("#6b7f86");
                            txt.Span(" / ").FontSize(8).FontColor("#6b7f86");
                            txt.TotalPages().FontSize(8).FontColor("#6b7f86");
                        });
                    });
            });
        }).GeneratePdf();

        var fileName = $"rezervasyon-{tour.Title?.Replace(" ", "-")}-{DateTime.Now:yyyyMMdd}.pdf";
        return File(pdf, "application/pdf", fileName);
    }

    private static void StatBox(RowDescriptor row, string label, string value, string color)
    {
        row.RelativeItem()
            .Border(1).BorderColor("#e0eaea")
            .Padding(10)
            .Column(c =>
            {
                c.Item().Text(label).FontSize(8).FontColor("#6b7f86").Bold();
                c.Item().Text(value).FontSize(16).Bold().FontColor(color);
            });
    }

    private static void TCell(TableDescriptor table, string bg, string text,
        string color, int size = 9, bool bold = false, bool center = false)
    {
        var cell = table.Cell()
            .Background(bg)
            .BorderBottom(1).BorderColor("#f0f0f0")
            .Padding(5).AlignMiddle();

        var aligned = center ? cell.AlignCenter() : cell.AlignLeft();
        var t = aligned.Text(text).FontSize(size).FontColor(color);
        if (bold) t.Bold();
    }
}