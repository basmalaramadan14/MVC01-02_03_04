using GymManagment.BLL.ViewModels;
using GymManagment.BLL.ViewModels.MemberViewModels;
using GymManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Services.Interfaces
{
    public interface IMemberService
    {
         Task<IEnumerable<MemberViewModel>>GetAllAsync(CancellationToken ct = default);

        //Crate Member
        Task<bool> CreateMemberAsync(CreateMemberViewModel member, CancellationToken ctt = default);
    }
}
