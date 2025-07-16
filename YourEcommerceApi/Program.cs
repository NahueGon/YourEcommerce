using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.Product.Examples;
using YourEcommerceApi.Services;
using YourEcommerceApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Db connection
var connectionString = builder.Configuration.GetConnectionString("cnYourEcommerce");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubcategoryService, SubcategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ISportService, SportService>();

// Swageer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();