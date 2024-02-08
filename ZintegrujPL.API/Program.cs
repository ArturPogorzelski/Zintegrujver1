using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ZintegrujPL.Core.Interfaces.Repositories;
using ZintegrujPL.DAL.Database;
using ZintegrujPL.Infrastructure.Repositories;
using ZintegrujPL.Services.MappingProfles;
using AutoMapper;
using ZintegrujPL.Core.Interfaces.Services;
using ZintegrujPL.Infrastructure.Services;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Konfiguracja EF
// Rejestracja AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Konfiguracja po³¹czenia dla Dappera
// Rejestracja DbConnectionFactory
builder.Services.AddScoped<IDbConnectionFactory>(provider =>
    new DbConnectionFactory(builder.Configuration.GetConnectionString("DefaultConnection")));


// Konfiguracja AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddHttpClient(); // Dodaje IHttpClientFactory do kontenera DI
// Rejestracja serwisów
builder.Services.AddScoped<IFileProcessingService, FileProcessingService>();
builder.Services.AddScoped<IFileDownloadService, FileDownloadService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Konfiguracja dla Swaggera (jeœli u¿ywany)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ZintegrujPL.API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZintegrujPL.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
