using gym.contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gym.Controllers
{
    public class PlanController : Controller
    {
        //1 Database connection
        private readonly GymDbContext context;
        public PlanController()
        {
            context = new GymDbContext();

        }
        //2 GET :: BaseUrl/Plan/Index

        public  async Task<IActionResult> Index()
        {
            var Plans = await context.plans.ToListAsync();

            return View(Plans);
        }
        //Get :: BaseUrl/plane/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var Plan = await context.plans.FindAsync(id);
            if (Plan == null)
                return RedirectToAction(nameof(Index));
                    return View(Plan);
            {
               
                


            }
        }
    }
}
