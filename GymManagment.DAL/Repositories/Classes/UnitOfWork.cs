using gym.contexts;
using GymManagment.DAL.Models;
using GymManagment.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _dbContext;
        private readonly Dictionary<String, object> _repositories = [];

       //Database Connection

       public UnitOfWork(GymDbContext dbContext, ISessionRepository sessionRepo)
        {
            _dbContext = dbContext;
            SessionRepository = sessionRepo;
        }

        public ISessionRepository SessionRepository { get; }
        public IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            //check if repo exist or not????
            //IGenericRepository<Member> => Name
            var TypeName = typeof(TEntity).Name;
            // if exist  in Dictionary => use it 
            if (_repositories.TryGetValue(TypeName, out object? value))
                return (IGenericRepository<TEntity>)value;

            //if not exsit 
            // create
            else
            {
                var repo = new GenericRepository<TEntity>(_dbContext);
                _repositories[TypeName ]= repo;

                 return repo;
            }


        }
        public IGenericRepository<T> GetRepository<T>() where T : BaseEntity, new()
        {
            return GetGenericRepository<T>();
        }



        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        
            => await _dbContext.SaveChangesAsync(ct);   
        

    }

}
