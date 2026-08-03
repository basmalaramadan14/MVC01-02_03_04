using GymManagment.BLL.Common;
using GymManagment.BLL.ViewModels;
using GymManagment.BLL.ViewModels.CreateSessionViewModel;
using GymManagment.BLL.ViewModels.SessionViewModel;
using GymManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Services.Interfaces
{
    public interface ISessionService
    {
        //GET ALL Sessions
        Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken ct = default);
        // create  Session
        Task<Result> createSessionAsync(CreateSessionViewModel model, CancellationToken ct = default);
        Task<IEnumerable<TrainerSelectViewModel>> GetTrainerForDropDown(CancellationToken ct = default);
        Task<IEnumerable<CategorySelectViewModel>> GetCategoryForDropDown(CancellationToken ct = default);

    }
}
