using BoardGameOfDayApi.Dtos;
using BoardGameOfDayApi.Services;
using OnlineStoreOfBoardGames.Data;
using OnlineStoreOfBoardGames.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.SetIsOriginAllowed(x => true);
        p.AllowCredentials();
    });
});

builder.Services.AddDbContext<PortalDbContext>();
builder.Services.AddScoped<BoardGameRepositories>();

builder.Services.AddSingleton<CacheService>();

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Board game of day api");
app.MapGet("/getBoardGameOfDay", (CacheService cache) => {

    var boardGame = cache.GetBoardGameOfDay();
    var dto = new DtoBoardGameOfDay
    {
        Id = boardGame.Id,
        Title = boardGame.Title,
        Price = boardGame.Price
    };

    return dto;
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
