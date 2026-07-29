using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace gym.PL.Controllers
{
    public class MembersController : Controller
    {

        //MemberService
        private readonly IMemberService _memService;


        public MembersController(IMemberService memService)
        {
            _memService = memService;
        }

        #region Get Member

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var members = await _memService.GetAllAsync(ct);
            return View(members);
        }



        #endregion


        #region Create
        //Get 
        [HttpGet]
        public IActionResult Create()
            => View();
        //Post
        //CreateMember

        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberViewModel model, CancellationToken ct)

        {
              //Cheeck ModelState
              if(!ModelState.IsValid) return View(nameof(Create), model);
               var result = await _memService.CreateMemberAsync(model, ct);

            if (result)
                TempData["SuccessMessage"] = "Member Create SuccessFully ";
            else
                TempData["ErrorMessage"] = "Member Failed To Create Member!";


            return RedirectToAction(nameof(Index));
          }
        #endregion


        #region Edit


        #endregion


        #region Delete


        #endregion


       
    }
}
