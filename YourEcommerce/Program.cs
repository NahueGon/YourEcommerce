using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using YourEcommerce.Config;
using YourEcommerce.Services;
using YourEcommerce.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

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
    });

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

void ConfigureHttpClient(IServiceProvider serviceProvider, HttpClient client)
{
    var apiSettings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;
    if (string.IsNullOrEmpty(apiSettings.BaseUrl)) throw new Exception("ApiSettings.BaseUrl no está configurado.");
    
    client.BaseAddress = new Uri(apiSettings.BaseUrl);
}

builder.Services.AddHttpClient<IUserService, UserService>(ConfigureHttpClient);
builder.Services.AddHttpClient<IAuthService, AuthService>(ConfigureHttpClient);
builder.Services.AddScoped<ICategoryService, CategoryService>();

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
