using gym.Models;
using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels;
using GymManagment.BLL.ViewModels.MemberViewModels;
using GymManagment.DAL.Models;
using GymManagment.DAL.Repositories;
using GymManagment.DAL.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Services
{
    public class MemberService : IMemberService
    {

        //DatabaseConnection

        private readonly IGenericRepository<Member> _memberRepo;
        private readonly IGenericRepository<MemberShip> _membershipRepo;
        private readonly IGenericRepository<Plan> _planRepo;
        private readonly IGenericRepository<HealthRecord> _healthRecordRepo;
        private readonly IGenericRepository<Booking> _bookingRepo;


        public MemberService(IGenericRepository<Member> memberRepo,
                                   IGenericRepository<MemberShip> membershipRepo,
                                     IGenericRepository<Plan> planRpo, 
                                     IGenericRepository<HealthRecord> HealthRecordRepo,
                                     IGenericRepository<Booking> bookingRepo)
         {
            _memberRepo = memberRepo;
            _membershipRepo = membershipRepo;
            _planRepo =  planRpo;
            _healthRecordRepo = HealthRecordRepo;
            _bookingRepo = bookingRepo;
        }
        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ctt = default)
        {

            //Email Exist or Not

            var EmailExist = await _memberRepo.AnyAsync(X => X.Email == model.Email);

            //phone Exist or Not
            var PhoneExist = await _memberRepo.AnyAsync(X => X.Phone == model.Phone);

            if (EmailExist || PhoneExist) return false;


            //Add Member
            var member = new Member()
            {

                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Gender = model.Gender,
                DateOfBith = model.DateOfBirth,
                Address = new Address()
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street,


                },
                HealthRecord = new HealthRecord()
                {
                    BloodType = model.HealthRecordViewModel.BloodType,
                    Heigth = model.HealthRecordViewModel.Height,
                    Weight = model.HealthRecordViewModel.Weight,
                    Note = model.HealthRecordViewModel.Note,

                }

            };
            var result = await _memberRepo.AddAsync(member);
            return result > 0;

        }

        public async Task<bool> DeleteMemberAsync(int memberId, CancellationToken ct = default)
        {

            var member = await _memberRepo.GetByIdAsync(memberId, ct);
            if (member is null) return false;

            //If Member
            var HasActiveBooking = await _bookingRepo.AnyAsync( B => B.MemberId == memberId && B.Session.StartDate > DateTime.Now);
            if(HasActiveBooking) return false;

            var result = await _memberRepo.DeleteAsync(member);

            return result >0 ;


        }

        public async Task<IEnumerable<MemberViewModel>> GetAllAsync(CancellationToken ct = default)
        {
            var members = await _memberRepo.GetAllAsync(ct : ct);
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
            var member = await _memberRepo.GetByIdAsync(memberId, ct );
            if(member == null) return null;
            //Table= Member
            //Return= Member
            var model = new MemberViewModel()
            {


                Name = member.Name,
                Phone = member.Phone,
                Photo = member.Photo,
                Email = member.Email,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBith.ToShortDateString(),
                Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}"
            };
            //cheeck
            var ActiveMembership = await _membershipRepo.FirstOrDefaultAsync(X=> X.MemberId== memberId && X.EndDate> DateTime .Now);  
             if (ActiveMembership is not null) 
            {
                // PlanName

                var Activeplan = await _planRepo.GetByIdAsync(ActiveMembership.PlanId, ct);
                model.PlanName = Activeplan.Name;
                model.MembershipStartDate = ActiveMembership.CreateAt.ToString();
                model.MembershipEndDate = ActiveMembership.EndDate.ToString();

            }
             return model;
        }

        public async Task<HealthRecordViewModel> GetMemberHealthRecord(int memberId, CancellationToken ct)
        {
            var record = await _healthRecordRepo.FirstOrDefaultAsync(X=> X.MemberId == memberId , ct: ct);
            if   (record is null) return null;

            else
                return new HealthRecordViewModel()
                {
                    Weight = record.Weight,
                    Height = record.Heigth,
                    BloodType = record.BloodType,
                    Note = record.Note,

                };
        }

        public async Task<MemberToUpdateViewModel> GetMemberToUpdateAsync(int memberId, CancellationToken ct = default)
        {
            var member = await _memberRepo.GetByIdAsync(memberId, ct);
            if (member is null) return null;
            else
                return new MemberToUpdateViewModel()
                {
                    Name = member.Name,
                    Phone = member.Phone,
                    Email = member.Email,
                    City = member.Address.City,
                    BuildingNumber = member.Address.BuildingNumber,
                    Street = member.Address.Street,
                    Photo = member.Photo,
                };
       }

        public async Task<bool> UpdateMemberAsync(int id, MemberToUpdateViewModel model, CancellationToken ct = default)
        {
            // Get 
            var member = await _memberRepo.GetByIdAsync(id, ct);

            //cheeck if any other user has the same phone or email
            var EmailExist = await _memberRepo.AnyAsync(M => M.Email == model.Email && M.Id != id);
            var PhoneExist = await _memberRepo.AnyAsync(M=>M.Phone==model.Phone&& M.Id!=id);
             if (EmailExist || PhoneExist) return false;
            member.Phone = model.Phone;
            member.Email = member.Email;
            member.Address.City= model.City;
            member.Address.Street= model.Street;
            member.Address.BuildingNumber= model.BuildingNumber;
            member.UpdateAt= DateTime.Now;
             var result = await _memberRepo.UpdateAsync(member);

            return result > 0;
        }

        
    }
}
