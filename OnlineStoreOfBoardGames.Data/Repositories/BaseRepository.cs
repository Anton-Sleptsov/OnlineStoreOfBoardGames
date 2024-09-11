using Microsoft.EntityFrameworkCore;
using OnlineStoreOfBoardGames.Data.Model;
using OnlineStoreOfBoardGames.Data.Repositories.Interfaces;
using OnlineStoreOfBoardGames.Data.Repositories.RawSql;
using System.Reflection;

namespace OnlineStoreOfBoardGames.Data.Repositories
{
    public abstract class BaseRepository<DbModel> : IBaseRepository<DbModel> where DbModel : BaseModel
    {
        protected PortalDbContext _dbContext;
        protected DbSet<DbModel> _dbSet;

        public BaseRepository(PortalDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<DbModel>();
        }

        public virtual bool Any()
            => _dbSet.Any();

        public virtual List<DbModel> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual DbModel? Get(int id)
        {
            return _dbSet.FirstOrDefault(dbModel => dbModel.Id == id);
        }

        public virtual DbModel Create(DbModel model)
        {
            _dbSet.Add(model);

            _dbContext.SaveChanges();

            return model;
        }

        public virtual void Delete(int id)
        {
            var model = Get(id);

            if (model is null)
            {
                throw new KeyNotFoundException();
            }

            Delete(model);
        }

        public virtual void Delete(DbModel model)
        {
            _dbSet.Remove(model);
            _dbContext.SaveChanges();
        }

        public IQueryable<T> CustomSqlQuery<T>(string sqlFileName)
        {
            var assembly = Assembly.GetAssembly(typeof(SqlQueryManager));
            var assemblyFilePath = assembly.Location;
            var assemblyPath = Path.GetDirectoryName(assemblyFilePath);
            var fileName = $"{sqlFileName}.sql";
            var path = Path.Combine(assemblyPath, "Repositories", "RawSql", fileName);
            var sql = File.ReadAllText(path);
            return _dbContext.Database.SqlQueryRaw<T>(sql);
        }
    }
}
