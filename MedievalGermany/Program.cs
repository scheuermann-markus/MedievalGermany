using FluentValidation;
using MediatR;
using MedievalGermany.Application.Indexes;
using MedievalGermany.Application.Interfaces;
using MedievalGermany.Application.Services;
using MedievalGermany.Components;
using MedievalGermany.Components.Pages;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Konfiguration von IMediator
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<ICastleService, CastleService>();
builder.Services.AddSingleton<IRavenDbService, RavenDbService>();
builder.Services.AddSingleton(e => e.GetRequiredService<IRavenDbService>().GetDocumentStore());
builder.Services.AddScoped<IValidator<UploadCastle.ViewModel>, UploadCastle.ViewModel.Validator>();


var app = builder.Build();

// Índexes erstellen
using (var scope = app.Services.CreateScope())
{
    var documentStore = scope.ServiceProvider.GetRequiredService<IDocumentStore>();
    await IndexCreation.CreateIndexesAsync(typeof(Castles_ByName).Assembly, documentStore);
}


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
