using GymManagment.BLL.Services.Interfaces;
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


        #endregion


        #region Edit


        #endregion


        #region Delete


        #endregion


       
    }
}
