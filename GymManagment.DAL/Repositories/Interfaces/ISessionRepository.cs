using GymManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.DAL.Repositories.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {

        //GET ALL , GETBYID, ADD,UPDATE ,DELETE => From IGeneric
        Task<IEnumerable<Session>> GetSessionsWithTrainerAndCategory(CancellationToken ct = default);
        Task<int> CountOfBookedSlotsAsync(int sessionId, CancellationToken ct = default);
    }
}
