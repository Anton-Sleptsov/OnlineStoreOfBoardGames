using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Data.Repositories;
using OnlineStoreOfBoardGames.Data.Repositories.Interfaces;
using System.Reflection;

namespace OnlineStoreOfBoardGames.Helpers
{
    public class AutoRegistrator
    {
        public void RegiterRepositories(IServiceCollection services)
        {
            var baseRepositoryGenericType = typeof(BaseRepository<>);
            var assembly = Assembly.GetAssembly(baseRepositoryGenericType);
            var repositoryTypes = assembly
                .GetTypes()
                .Where(t => t.IsClass
                    && t.BaseType != null
                    && t.BaseType.IsGenericType
                    // t.BaseType == BaseRepository<BoardGame>
                    // t.BaseType.GetGenericTypeDefinition() == BaseRepository<>
                    && t.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>))
                .ToList();

            // services.AddScoped<GameRepositories>();
            repositoryTypes.ForEach(type => services.AddScoped(type));
        }

        public void RegiterRepositoriesByInterface(IServiceCollection services)
        {
            var baseRepositoryGenericType = typeof(BaseRepository<>);
            var assembly = Assembly.GetAssembly(baseRepositoryGenericType);
            var repositoryTypes = assembly
                .GetTypes()
                .Where(t => t.IsClass
                    && t.BaseType != null
                    && t.BaseType.IsGenericType
                    && t.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>))
                .ToList();
            
            repositoryTypes.ForEach(classType =>
            {
                var interfaceType = classType.GetInterfaces()
                    .FirstOrDefault(interfaceType =>
                        interfaceType
                        .GetInterfaces()
                        .Any(x => x.IsGenericType
                            && x.GetGenericTypeDefinition() == typeof(IBaseRepository<>)));

                if (interfaceType != null )
                {
                    services.AddScoped(interfaceType, classType);
                }
            });
        }
    }
}
