using gym.contexts;
using GymManagment.DAL.Models;
using GymManagment.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        //Database Connection

        private readonly GymDbContext _dbContext;

        public SessionRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

        }

        

        public async Task<int> CountOfBookedSlotsAsync(int sessionId, CancellationToken ct = default)
        {
            
            return await _dbContext.Bookings.AsNoTracking().CountAsync(B => B.SessionId == sessionId);
        }

        public async Task<IEnumerable<Session>> GetSessionsWithTrainerAndCategory(CancellationToken ct = default)
        {
            var query = _dbContext.Sessions.AsNoTracking().Include(S => S.Trainer).Include(S => S.Category);
            return await query.ToListAsync();
        }
    }
}
