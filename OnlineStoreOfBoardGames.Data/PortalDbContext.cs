using Microsoft.EntityFrameworkCore;
using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Data.Model.Alerts;

namespace OnlineStoreOfBoardGames.Data
{
    public class PortalDbContext : DbContext
    {
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public PortalDbContext() { }
        public PortalDbContext(DbContextOptions<PortalDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql($"Host={DatabaseSettings.DbHost};Username={DatabaseSettings.DbUsername};Password={DatabaseSettings.DbPassword};Database={DatabaseSettings.DbDbName}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.FavoriteBoardsGames)
                .WithMany(x => x.UsersWhoFavoriteThisBoardGame);

            modelBuilder.Entity<User>()
                .HasMany(x => x.AlertsWhichISaw)
                .WithOne(x => x.User);

            modelBuilder.Entity<Alert>()
                .HasMany(x => x.UsersWhoAlreadySawIt)
                .WithOne(x => x.Alert);

            modelBuilder.Entity<Order>()
                  .HasMany(x => x.Games)
                  .WithOne(x => x.Order)
                  .HasForeignKey(x => x.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.BoardGame)
                .WithMany()
                .HasForeignKey(x => x.BoardGameId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
