using HelpDeskMini.Data;
using HelpDeskMini.Services;
using HelpDeskMini.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.MigrationsAssembly("HelpDeskMini.Data")));

builder.Services.AddScoped<IHelpRequestService, HelpRequestService>();

var app = builder.Build();

// Миграциите се прилагат ръчно от Package Manager Console с Update-Database.
// Така грешките при свързване с SQL Server се виждат отделно от стартирането на MVC.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Requests/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Requests}/{action=Index}/{id?}");

app.Run();
