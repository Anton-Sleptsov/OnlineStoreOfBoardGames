using Microsoft.Extensions.DependencyInjection;
using OnlineStoreOfBoardGames.Data.Enums;
using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Data.Repositories;

namespace OnlineStoreOfBoardGames.Data
{
    public class Seed
    {
        public void Fill(IServiceProvider serviceProvider)
        {
            using var service = serviceProvider.CreateScope();

            FillUsers(service);
            FillBoardGames(service);
        }

        private void FillBoardGames(IServiceScope service)
        {
            var boardGameRepositories = service.ServiceProvider.GetService<BoardGameRepositories>()!;

            if (!boardGameRepositories.Any())
            {
                var ticketToRide = new BoardGame
                {
                    Title = "Ticket to Ride: Европа",
                    MiniTitle = "Постройте железные дороги по всей Европе!",
                    Description = "Эта увлекательная игра предлагает захватывающее путешествие из дождливого Эдинбурга в солнечный Константинополь. В настольной игре \"Ticket To Ride: Европа\" Вы посетите величественные европейские города, осталось только взять билет на поезд.",
                    Essence = "\"Билет на поезд: Европа\" (Ticket to Ride: Europe) стала второй в серии настольный игр о путешествиях по железной дороге. Здесь вы можете прокладывать маршруты, соединяя города, пускать новые составы и при случае обгонять соперников по количеству заработанных очков. В настольной игре \"Билет на поезд: Европа\" вы перенесетесь в самые красивые города. В новой игре в распоряжении игрока несколько оригинальных механик, добавились игровые элементы, и более разнообразными стали правила. Невероятные ощущения, динамика и новые открытия ждут вас в этой настольной игре.",
                    Tags = "Игра из серии",
                    Price = 3900,
                    ProductCode = 31458
                };
                boardGameRepositories.Create(ticketToRide);
            }
        }

        private void FillUsers(IServiceScope service)
        {
            var userRepository = service.ServiceProvider.GetService<UserRepository>()!;

            if (!userRepository.Any())
            {
                var admin = new User
                {
                    UserName = "admin",
                    Password = "admin",
                    Role = UserRole.Admin,
                    Language = Language.En
                };
                userRepository.Create(admin);

                var user = new User
                {
                    UserName = "user",
                    Password = "user",
                    Role = UserRole.User,
                    Language = Language.Ru
                };
                userRepository.Create(user);

                var boardGameCreator = new User
                {
                    UserName = "boardGameCreator",
                    Password = "1",
                    Role = UserRole.BoardGameAdmin,
                    Permission = Permission.CanCreateAndUpdateBoardGames,
                    Language = Language.Ru
                };
                userRepository.Create(boardGameCreator);

                var boardGameModerator = new User
                {
                    UserName = "boardGameModerator",
                    Password = "1",
                    Role = UserRole.BoardGameAdmin,
                    Permission = Permission.CanDeleteBoardGames,
                    Language = Language.Ru
                };
                userRepository.Create(boardGameModerator);

            }
        }

    }
}
