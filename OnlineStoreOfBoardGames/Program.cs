using Microsoft.EntityFrameworkCore;
using OnlineStoreOfBoardGames;
using OnlineStoreOfBoardGames.Controllers;
using OnlineStoreOfBoardGames.CustomMiddlewareServices;
using OnlineStoreOfBoardGames.Data;
using OnlineStoreOfBoardGames.Data.CacheServices;
using OnlineStoreOfBoardGames.Helpers;
using OnlineStoreOfBoardGames.Hubs;
using OnlineStoreOfBoardGames.Mappers;
using OnlineStoreOfBoardGames.Services;
using OnlineStoreOfBoardGames.Services.Apis;
using OnlineStoreOfBoardGames.Services.AuthStuff;
using OnlineStoreOfBoardGames.Services.AuthStuff.Interfaces;
using OnlineStoreOfBoardGames.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(x =>
{
    x.AddDefaultPolicy(p =>
    {
        p.AllowAnyMethod();
        p.AllowAnyOrigin();
        p.AllowAnyHeader();
    });
});

builder.Services
    .AddAuthentication(AuthController.AUTH_METHOD)
    .AddCookie(AuthController.AUTH_METHOD, option =>
    {
        option.LoginPath = "/Auth/Login";
        option.AccessDeniedPath = "/Auth/AccessDenied";
    });

builder.Services.AddDbContext<PortalDbContext>();

//Repository
var autoRegistrator = new AutoRegistrator();
autoRegistrator.RegiterRepositories(builder.Services);
autoRegistrator.RegiterRepositoriesByInterface(builder.Services);

builder.Services.AddSingleton<BoardGameCache>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<BoardGameMapper>();
builder.Services.AddScoped<AlertMapper>();

builder.Services.AddSingleton<IPathHelper, PathHelper>();
builder.Services.AddSingleton<PathHelper>();

builder.Services.AddScoped<LocalizatoinService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSignalR();

builder.Services.AddHttpClient<HttpBoardGamesReviewsApiService>(
    x => x.BaseAddress = new Uri($"http://{MicroservicesSettings.ReviewsHost}/"));
builder.Services.AddHttpClient<HttpBoardGameOfDayServise>(
    t => t.BaseAddress = new Uri($"http://{MicroservicesSettings.BoardGameOfDayHost}/"));
builder.Services.AddHttpClient<HttpBestBoardGameServise>(
    t => t.BaseAddress = new Uri($"http://{MicroservicesSettings.BestBoardGameHost}/"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PortalDbContext>();
    dbContext.Database.Migrate();
}

//app.UseMiddleware<MyGlobalHandlerException>();

var seed = new Seed();
seed.Fill(app.Services);

app.UseCors();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Who I am?
app.UseAuthorization(); // May I?

app.UseMiddleware<LocalizationMiddleware>();

app.MapHub<BoardGameHub>("/hubs/boardGame");
app.MapHub<AlertHub>("/hubs/alert");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BoardGame}/{action=Index}/{id?}");

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

app.Run();
