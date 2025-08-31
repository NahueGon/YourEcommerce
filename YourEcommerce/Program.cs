using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using YourEcommerce.Config;
using YourEcommerce.Repositories;
using YourEcommerce.Repositories.Interfaces;
using YourEcommerce.Services;
using YourEcommerce.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllersWithViews();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/auth/login";
        options.AccessDeniedPath = "/auth/AccessDenied";
    });

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

void ConfigureHttpClient(IServiceProvider serviceProvider, HttpClient client)
{
    var apiSettings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;
    if (string.IsNullOrEmpty(apiSettings.BaseUrl))
        throw new Exception("ApiSettings.BaseUrl no está configurado.");

    client.BaseAddress = new Uri(apiSettings.BaseUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}

builder.Services.AddSingleton(new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true,
    Converters = { new JsonStringEnumConverter() }
});

// Repositories
builder.Services.AddHttpClient<ISportRepository, SportRepository>(ConfigureHttpClient);
builder.Services.AddHttpClient<IBrandRepository, BrandRepository>(ConfigureHttpClient);
builder.Services.AddHttpClient<IGenderRepository, GenderRepository>(ConfigureHttpClient);
builder.Services.AddHttpClient<IUserRepository, UserRepository>(ConfigureHttpClient);
builder.Services.AddHttpClient<IProductRepository, ProductRepository>(ConfigureHttpClient);
builder.Services.AddHttpClient<ICategoryRepository, CategoryRepository>(ConfigureHttpClient);

builder.Services.AddHttpClient<IAuthService, AuthService>(ConfigureHttpClient);

// Services
builder.Services.AddScoped<ISportService, SportService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IGenderService, GenderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
.WithStaticAssets();

app.Run();
