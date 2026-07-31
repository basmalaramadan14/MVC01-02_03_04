using GymManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        //Get Repository
        IGenericRepository<TEntity>GetGenericRepository<TEntity>() where TEntity : BaseEntity , new();
        //SaveChanges
        Task<int> SaveChangesAsync(CancellationToken ct = default);

    }
}
