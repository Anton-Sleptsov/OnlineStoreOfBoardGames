using BestBoardGameApi.Dtos;
using OnlineStoreOfBoardGames.Data;
using OnlineStoreOfBoardGames.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
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

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Best board game api");
app.MapGet("/getBestBoardGame", (BoardGameRepositories repository) => {

    var boardGame = repository.GetBest();
    var dto = new DtoBestBoardGame
    {
        Id = boardGame.Id,
        Title = boardGame.Title,
        Price = boardGame.Price,
        CountOfUserWhoLikeIt = boardGame.CountOfUserWhoLikeIt
    };

    return dto;
});

app.Run();
