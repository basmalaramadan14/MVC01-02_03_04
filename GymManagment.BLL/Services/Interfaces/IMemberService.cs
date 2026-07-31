using GymManagment.BLL.ViewModels;
using GymManagment.BLL.ViewModels.MemberViewModels;

namespace GymManagment.BLL.Services.Interfaces
{
    public interface IMemberService
    {
         Task<IEnumerable<MemberViewModel>>GetAllAsync(CancellationToken ct = default);

        //Crate Member
        Task<bool> CreateMemberAsync(CreateMemberViewModel member, CancellationToken ctt = default);
        //Get Member
        Task<MemberViewModel?> GetMemberDetailsByAsync(int memberId, CancellationToken ct = default);
        //Get Member Health Record
        Task<HealthRecordViewModel> GetMemberHealthRecord(int memberId, CancellationToken ct );
        // Get Member To Update
        Task<MemberToUpdateViewModel>GetMemberToUpdateAsync( int memberId,CancellationToken ct = default );
        //Update 
        Task<bool> UpdateMemberAsync( int id, MemberToUpdateViewModel model , CancellationToken ct = default );
        //Delete

        Task<bool> DeleteMemberAsync(int memberId, CancellationToken ct = default);

    }
}
