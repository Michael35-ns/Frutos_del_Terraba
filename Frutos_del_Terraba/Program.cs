using Frutos_del_Terraba.Helpers.Implementaciones;
using Frutos_del_Terraba.Helpers.Interfaces;
using Frutos_del_Terraba.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configuración de los servicios
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddDistributedMemoryCache();


builder.Services.AddHttpClient("ApiBaseClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7240/api/"); 
});


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.MapRazorPages();

app.Run();