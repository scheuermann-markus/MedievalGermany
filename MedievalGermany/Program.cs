using FluentValidation;
using MediatR;
using MedievalGermany.Application.Interfaces;
using MedievalGermany.Application.Services;
using MedievalGermany.Components;
using MedievalGermany.Components.Pages;
using System.Reflection;
using MedievalGermany.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Choose connection string based on environment
string connectionString = builder.Environment.IsDevelopment()
    ? "RemoteConnection"
    : "LocalConnection";

builder.Services.AddMediatR(Assembly.GetExecutingAssembly()); // IMediator
builder.Services.AddScoped<ICastleService, CastleService>();
builder.Services.AddScoped<IValidator<UploadCastle.ViewModel>, UploadCastle.ViewModel.Validator>();

// PostgreSQL Connection hinzufügen
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
