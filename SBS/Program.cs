using SBS.Models;
using Microsoft.EntityFrameworkCore;
using SBS.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // register MVC
builder.Services.AddDbContext<SbsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// register services for DI
builder.Services.AddScoped<SignupService>(); // one instance per HTTP request
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// enable serving static files from wwwroot
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.MapGet("/", context =>
{
    context.Response.Redirect("/index.html");
    return Task.CompletedTask;
});

app.Run();

