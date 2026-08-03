using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels.CreateSessionViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace gym.PL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionServ;

        //Session Service
        public SessionController(ISessionService sessionServ)
        {
            _sessionServ = sessionServ;
        }

        //GET:: BaseUrl/Session/aIndex

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var sessions = await _sessionServ.GetAllSessionsAsync(ct);
            return View(sessions);
        }

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create() 
            {
            await DropDownList();
            return View();
            }

        [HttpPost]
        public async Task<IActionResult> create(CreateSessionViewModel model, CancellationToken ct)

        {
            //Cheeck model state
            if (!ModelState.IsValid)
            {
                await DropDownList();
                return View(model);
            }
          var result = await _sessionServ.createSessionAsync(model, ct);    
            if(result.success)
            {

                TempData["SuccesMessage"] = "Session Created";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = result.error;
            await DropDownList();
            return View(model);   
            
        }
        private async Task DropDownList()
        {
            ViewBag.Trainers = new SelectList(await _sessionServ.GetTrainerForDropDown(), "Id", "Name");
            ViewBag.Categories = new SelectList(await _sessionServ.GetCategoryForDropDown(), "Id", "CategoryName");



        }
        #endregion
    }

}
