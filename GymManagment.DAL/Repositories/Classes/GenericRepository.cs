using gym.contexts;
using GymManagment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymManagment.DAL.Repositories;
using System.Linq.Expressions;


namespace GymManagment.DAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()

    {
        //Database COnnection
        private readonly GymDbContext _dbContext;
        private readonly DbSet<TEntity> _set;

        public GenericRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
            //Register Service in program.cs
            _set = _dbContext.Set<TEntity>();


        }

        public async void AddAsync(TEntity entity)
        {
            _set. Add(entity);// Add Local
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        {
            return _set.AsNoTracking().AnyAsync(predicate, ct);
        }

        public async void DeleteAsync(TEntity entity)
        {
            _set .Remove(entity);


        }
    

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = false, CancellationToken ct = default)
        {
            IQueryable<TEntity> Query = tracking?_set :  _set.AsNoTracking();
            return await Query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false, CancellationToken ct = default)
        {
            IQueryable<TEntity> query = tracking ? _set : _set.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken ct = default)
        => await _set.FindAsync(id, ct);
        

        public async void UpdateAsync(TEntity entity)
        {
            _set .Update(entity);
        }

        
    }
}
