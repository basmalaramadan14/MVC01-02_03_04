using gym.contexts;
using gym.Models;
using GymManagment.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Repositories.Classes
{
    public class PlanRepository : IPlanRepository
    {
        //Database connection
        private readonly GymDbContext dbContext;

        public PlanRepository()
        {
            dbContext = new GymDbContext();
        }
        public async Task<int> AddAsync(Plan plan, CancellationToken ct = default)
        {
            dbContext.plans.Add(plan);
            return await dbContext.SaveChangesAsync(ct);
        }

        public async Task<int> DeleteAsync(Plan plan, CancellationToken ct = default)
        {
            dbContext.plans.Remove(plan);
            return await dbContext.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<Plan>> GetAllAsync(bool tracking = false, CancellationToken ct = default)
        {

            //if (tracking)// true: Enble tracking for update
            //    return await dbContext.plans.ToListAsync(ct);

            //else
            //    return await dbContext.plans.AsNoTracking().ToListAsync(ct);

            IQueryable<Plan> query = tracking? dbContext.plans: dbContext.plans.AsNoTracking();
            return await query.ToListAsync(ct);

        }

        public async Task<Plan?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await dbContext.plans.FindAsync(id, ct);
        }

        public Task<int> UpdateAsync(Plan plan, CancellationToken ct = default)
        {
            dbContext.plans.Update(plan);
            return dbContext.SaveChangesAsync(ct);
        }
    }
}
