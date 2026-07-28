using gym.contexts;
using gym.Models;
using GymManagment.DAL.Repositories;
using GymManagment.DAL.Repositories.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace gym.Controllers
{
    public class PlanController : Controller
    {
        private readonly IGenericRepository<Plan>  _planRepository;

        public PlanController(IGenericRepository<Plan> planRepository)
        {
             _planRepository = planRepository;

            }
        //2 GET :: BaseUrl/Plan/Index

        public  async Task<IActionResult> Index(CancellationToken ct)
        {
            var Plans = await _planRepository.GetAllAsync(ct : ct); // pass By name

            return View(Plans);
        }
        //Get :: BaseUrl/plane/Details/{id}
        public async Task<IActionResult> Details(int id , CancellationToken ct)
        {
            var Plan = await _planRepository.GetByIdAsync(id, ct);
            if (Plan == null)
                return RedirectToAction(nameof(Index));
                    return View(Plan);
            {
               
                


            }
        }
    }
}
