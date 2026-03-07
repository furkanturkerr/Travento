using System.Reflection;
using Microsoft.Extensions.Options;
using Project3Travelin.Services.AboutService;
using Project3Travelin.Services.BannerService;
using Project3Travelin.Services.BookingServices;
using Project3Travelin.Services.CategoryServices;
using Project3Travelin.Services.CommantServices;
using Project3Travelin.Services.ContactService;
using Project3Travelin.Services.FaqServices;
using Project3Travelin.Services.InstagramServices;
using Project3Travelin.Services.PopulerService;
using Project3Travelin.Services.SliderServices;
using Project3Travelin.Services.TourServices;
using Project3Travelin.Services.TourTineraryService;
using Project3Travelin.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ITineraryService, TineraryService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IPopulerService, PopulerService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IBannerService, BannerService>();
builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IInstagramService, InstagramService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "iletisim",
    pattern: "iletisim",
    defaults: new { controller = "Contact", action = "Iletisim" });

app.MapControllerRoute(
    name: "rezervasyon",
    pattern: "rezervasyon",
    defaults: new { controller = "Booking", action = "Rezervasyon" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Anasayfa}/{id?}");



app.Run();