using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        public Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken ct = default)
        {
            //Session. tranier
            //lazy
        }
    }
}
