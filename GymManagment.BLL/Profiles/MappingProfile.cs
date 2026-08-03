using AutoMapper;
using GymManagment.BLL.ViewModels;
using GymManagment.BLL.ViewModels.CreateSessionViewModel;
using GymManagment.BLL.ViewModels.MemberViewModels;
using GymManagment.BLL.ViewModels.SessionViewModel;
using GymManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MemberProfiles();
            SessionProfiles();

           

        }
            private void MemberProfiles()
        {
            CreateMap<Member, MemberViewModel>()
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"))
           .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBith.ToShortDateString()));

            CreateMap<HealthRecord, HealthRecordViewModel>().ReverseMap();

            CreateMap<Member, MemberToUpdateViewModel>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber));


            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.City = src.City;
                    dest.Address.Street = src.Street;
                });

            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address()
                {
                    BuildingNumber = src.BuildingNumber,
                    City = src.City,
                    Street = src.Street,


                }))

                .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecordViewModel));


        }
            private void SessionProfiles()
        {
            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Category, CategorySelectViewModel>();
            CreateMap<Trainer, TrainerSelectViewModel>();

        }


    }
    }

