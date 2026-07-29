using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels;
using GymManagment.BLL.ViewModels.MemberViewModels;
using GymManagment.DAL.Models;
using GymManagment.DAL.Repositories;
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

        public MemberService(IGenericRepository<Member> memberRepo)
         {
            _memberRepo = memberRepo;
        }

        //public async Task<bool> CreateMemberAsy(CreateMemberViewModel model, CancellationToken ctt = default)
        //{
        //    //Email Exist or Not

        //    var EmailExist =  await _memberRepo.AnyAsync(X =>X.Email == model.Email);

        //    //phone Exist or Not
        //    var PhoneExist = await _memberRepo.AnyAsync(X => X.Phone == model.Phone);

        //    if (EmailExist || PhoneExist) return false;


        //    //Add Member
        //    var member = new Member()
        //    {

        //        Name = model.Name,
        //        Email = model.Email,
        //        Phone = model.Phone,
        //        Gender = model.Gender,
        //        DateOfBith = model.DateOfBirth,
        //        Address = new Address()
        //        {
        //            BuildingNumbern = model.BuildingNumber,
        //            City = model.City,
        //            Street = model.Street,


        //        },
        //        HealthRecord = new HealthRecord()
        //        {
        //            BloodType = model.HealthRecordViewModel.BloodType,
        //            Heigth = model.HealthRecordViewModel.Height,
        //            Weight = model.HealthRecordViewModel.Weight,
        //            Note = model.HealthRecordViewModel.Note,

        //        }

        //    };
        //        var  result = await _memberRepo.AddAsync(member);
        //    return result > 0;
        //}
        //public async Task<bool> CreateMemberAsy(CreateMemberViewModel model, CancellationToken ctt = default)

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
                    BuildingNumbern = model.BuildingNumber,
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
    }
}
