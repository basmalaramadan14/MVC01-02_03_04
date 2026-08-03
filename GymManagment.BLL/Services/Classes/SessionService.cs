using AutoMapper;
using GymManagment.BLL.Common;
using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels.CreateSessionViewModel;
using GymManagment.BLL.ViewModels.SessionViewModel;
using GymManagment.DAL.Models;
using GymManagment.DAL.Models.Enums;
using GymManagment.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagment.BLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Result> createSessionAsync(CreateSessionViewModel model, CancellationToken ct = default)
        {

            //Valdations
            if (model.EndDate <= model.StartDate) return Result.Validation("End Date Must Be Greate Than Start Date");
            if (model.StartDate <= DateTime.Now) return Result.Validation("Start Date Must Be in Future"); ;
            if (model.Capacity < 1 || model.Capacity > 25) return Result.Validation("Capacity Must Be Between 1 and 25 ");
            //Get Trainer
            var trainer = await _unitOfWork.GetRepository<Trainer>().GetByIdAsync(model.TrainerId);
            if (trainer is null) return Result.Validation("Trainer Not Found");
            //Get Category
            var Category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(model.CategoryId);
            if (Category is null) return Result.Validation("Category Not Found");
            //cheeck
            var isValid = Enum.TryParse<Specialty>(Category.CategoryName, true, out var CategorySpecialty);
            if (!isValid || trainer.Specialty != CategorySpecialty) return Result.Validation("Trainer and Category Must be the same! ");


            //CreateSession
            var session = _mapper.Map<Session>(model);

            _unitOfWork.GetRepository<Session>().AddAsync(session);   
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0? Result.Ok() : Result.Fail("Failed to Create Session");
        }

        public async Task<IEnumerable<SessionViewModel>> GetAllSessionsAsync(CancellationToken ct = default)
        {
           var sessions = await _unitOfWork.SessionRepository.GetSessionsWithTrainerAndCategory(ct);
            if (sessions == null || !sessions.Any()) return null;

            var mappedSession = sessions.Select(S=> new SessionViewModel()
            { 
                Id = S.Id,
                Capacity = S.Capacity,
                CategoryName =S.Category.CategoryName,
                TrainerName=S.Trainer.Name,
                StartDate =S.StartDate,
                EndDate = S.EndDate,
            
            });
            //Booking Slots
            foreach(var session in mappedSession)                             
            {
                session.AvailableSlots = session.Capacity - await _unitOfWork.SessionRepository.CountOfBookedSlotsAsync(session.Id,ct);
                   
            }
            return mappedSession;
        }

        public async Task<IEnumerable<CategorySelectViewModel>> GetCategoryForDropDown(CancellationToken ct = default)
        {
            var result = await _unitOfWork.GetRepository<Category>().GetAllAsync(ct: ct);
            return _mapper.Map< IEnumerable<CategorySelectViewModel>>(result);
       }

        public async Task<IEnumerable<TrainerSelectViewModel>> GetTrainerForDropDown(CancellationToken ct = default)
        {
            var result = await _unitOfWork.GetRepository<Trainer>().GetAllAsync(ct: ct);
            return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(result);
        }

       
    }
}
