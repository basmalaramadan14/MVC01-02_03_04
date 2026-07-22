using gym.contexts;
using GymManagment.DAL.Repositories.Classes;
using GymManagment.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gym.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanRepository _PlanRepository;
        public PlanController(IPlanRepository planRepository)
        {
             _PlanRepository = planRepository;

            }
        //2 GET :: BaseUrl/Plan/Index

        public  async Task<IActionResult> Index(CancellationToken ct)
        {
            var Plans = await _PlanRepository.GetAllAsync(ct : ct); // pass By name

            return View(Plans);
        }
        //Get :: BaseUrl/plane/Details/{id}
        public async Task<IActionResult> Details(int id , CancellationToken ct)
        {
            var Plan = await _PlanRepository.GetByIdAsync(id, ct);
            if (Plan == null)
                return RedirectToAction(nameof(Index));
                    return View(Plan);
            {
               
                


            }
        }
    }
}
