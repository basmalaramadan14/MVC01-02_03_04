using AutoMapper;
using gym.Models;
using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels;
using GymManagment.BLL.ViewModels.MemberViewModels;
using GymManagment.DAL.Models;
using GymManagment.DAL.Repositories;
using GymManagment.DAL.Repositories.Classes;
using GymManagment.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Services.Classes
{
    public class MemberService : IMemberService
        
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberService (IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IUnitOfWork UnitOfWork { get; }

        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {

            //Email Exist or Not

            var EmailExist = await _unitOfWork.GetGenericRepository<Member>().AnyAsync(X => X.Email == model.Email);

            //phone Exist or Not
            var PhoneExist = await _unitOfWork.GetGenericRepository<Member>().AnyAsync(X => X.Phone == model.Phone);

            if (EmailExist || PhoneExist) return false;


            //Add Member
            var member =_mapper.Map<Member>(model); 
            
              _unitOfWork.GetGenericRepository<Member>().AddAsync(member);
             var result  = await _unitOfWork.SaveChangesAsync();
            return result > 0;

        }

        public async Task<bool> DeleteMemberAsync(int memberId, CancellationToken ct = default)
        {

            var member = await _unitOfWork.GetGenericRepository<Member>().GetByIdAsync(memberId, ct);
            if (member is null) return false;

            //If Member
            var HasActiveBooking = await _unitOfWork.GetGenericRepository<Booking>().AnyAsync( B => B.MemberId == memberId && B.Session.StartDate > DateTime.Now);
            if(HasActiveBooking) return false;

             _unitOfWork.GetGenericRepository<Member>().DeleteAsync(member);
             var result = await _unitOfWork .SaveChangesAsync();
            return result >0 ;


        }

        public async Task<IEnumerable<MemberViewModel>> GetAllAsync(CancellationToken ct = default)
        {
            var members = await _unitOfWork.GetGenericRepository<Member>().GetAllAsync(ct : ct);
            //Members Comes From Database   

            if (!members.Any()) return [];
            //Member => ViewModel
            List<MemberViewModel> memberVM = new List<MemberViewModel>();

            foreach (var member in members)
            {
                //Data comes fropm Database i need to send ot to ViewModel
                //Manual Mapping
                var memberViewModel = new MemberViewModel()
                {
                    Name = member.Name,
                    Phone = member.Phone,
                    Photo = member.Photo,
                    Email = member.Email,
                    Id = member.Id,
                    Gender = member.Gender.ToString(),
                };
                memberVM.Add(memberViewModel);
            }
            return memberVM;
        }

        public async Task<MemberViewModel?> GetMemberDetailsByAsync(int memberId, CancellationToken ct = default)
        {
            //Get Member
            var member = await _unitOfWork.GetGenericRepository<Member>().GetByIdAsync(memberId, ct );
            if(member == null) return null;
            //Table= Member
            //Return= Member
            var model = _mapper.Map<Member, MemberViewModel>(member);
 
            //cheeck
            var ActiveMembership = await _unitOfWork.GetGenericRepository<MemberShip>().FirstOrDefaultAsync(X=> X.MemberId== memberId && X.EndDate> DateTime .Now);  
             if (ActiveMembership is not null) 
            {
                // PlanName

                var Activeplan = await _unitOfWork.GetGenericRepository<Plan>().GetByIdAsync(ActiveMembership.PlanId, ct);
                model.PlanName = Activeplan.Name;
                model.MembershipStartDate = ActiveMembership.CreateAt.ToString();
                model.MembershipEndDate = ActiveMembership.EndDate.ToString();

            }
             return model;
        }

        public async Task<HealthRecordViewModel> GetMemberHealthRecord(int memberId, CancellationToken ct)
        {
            var record = await _unitOfWork.GetGenericRepository<HealthRecord>().FirstOrDefaultAsync(X => X.MemberId == memberId, ct: ct);
            if (record is null) return null;

            else
                return _mapper.Map<HealthRecordViewModel>(record);


        }         

        public async Task<MemberToUpdateViewModel> GetMemberToUpdateAsync(int memberId, CancellationToken ct = default)
        {
            var member = await _unitOfWork.GetGenericRepository<Member>().GetByIdAsync(memberId, ct);
            if (member is null) return null;
            else
                return  _mapper.Map<MemberToUpdateViewModel>(member);
       }

        public async Task<bool> UpdateMemberAsync(int id, MemberToUpdateViewModel model, CancellationToken ct = default)
        {
            // Get 
            var member = await _unitOfWork.GetGenericRepository<Member>().GetByIdAsync(id, ct);

            //cheeck if any other user has the same phone or email
            var EmailExist = await _unitOfWork.GetGenericRepository<Member>().AnyAsync(M => M.Email == model.Email && M.Id != id);
            var PhoneExist = await _unitOfWork.GetGenericRepository<Member>().AnyAsync(M=>M.Phone==model.Phone&& M.Id!=id);

             if (EmailExist || PhoneExist) return false;

            _mapper.Map<Member>(model); 
            member.UpdateAt= DateTime.Now;




             _unitOfWork.GetGenericRepository<Member>().UpdateAsync(member);
             var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        
    }
}
