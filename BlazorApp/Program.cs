using BlazorApp.Components;
using BlazorApp.Infraestructure.Data;
using BlazorApp.Models;
using BlazorApp.Models.BaseDeDatos;
using BlazorApp.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Hubs;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

// agregado para utilizar SignalR
builder.Services.AddSignalR();

builder.Services.AddScoped<IOficinaRepository, OficinaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IOperarioRepository, OperarioRepository>();


builder.Services.AddDbContext<ContextoBD>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ContextoBD>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

/*
app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapHub<NotificacionesHub>("/atencionHub"); // Añade esta línea para mapear el hub
    endpoints.MapFallbackToPage("/_Host");
});
*/

app.Run();