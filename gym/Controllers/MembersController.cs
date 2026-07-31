using GymManagment.BLL.Services.Interfaces;
using GymManagment.BLL.ViewModels;
using GymManagment.BLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        //GET
        public async Task<IActionResult> MemberDetails(int id, CancellationToken ct)
        {
            var member = await _memService.GetMemberDetailsByAsync(id, ct);
            if (member is null)
            {
                TempData["ErrorMrssage"] = "Member Not Found !";
            }
            return View(member);

        }
        public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken ct)
        {
            var record = await _memService.GetMemberHealthRecord(id, ct);

            if (record is null)
            {
                TempData["ErrorMessage"] = "No Health Recordfound!";
                return RedirectToAction(nameof(Index)); ;
            }
            return View(record);
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
            if (!ModelState.IsValid) return View(nameof(Create), model);
            var result = await _memService.CreateMemberAsync(model, ct);

            if (result)
                TempData["SuccessMessage"] = "Member Create SuccessFully ";
            else
                TempData["ErrorMessage"] = "Member Failed To Create Member!";


            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Edit

        [HttpGet]
        public async Task<IActionResult> EditMember(int id, CancellationToken ct)
        {
            var member = await _memService.GetMemberToUpdateAsync(id, ct);

            if (member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found !";
                return RedirectToAction(nameof(Index));


            }
            return View(member);

        }


        [HttpPost]

        public async Task<IActionResult> EditMember (int id , MemberToUpdateViewModel model, CancellationToken ct)
        {
            //Cheeck Models State
            if (!ModelState.IsValid) return View(model);

            var result = await _memService.UpdateMemberAsync(id, model, ct);

            if (result)

                TempData["SuccessMessage"] = "Member Updated Successfully";
            else

                TempData["ErrorMessage"] = "Faild To Update Member";
            return RedirectToAction(nameof(Index));

        }



        #endregion


        #region Delete
        public async Task<IActionResult> Delete (int id , CancellationToken ct )
        {

            var member = await _memService.GetMemberToUpdateAsync(id, ct);

            if (member is null)

             {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));

            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {

            var result = await _memService.DeleteMemberAsync(id, ct);
            if (result)
                TempData["SuccessMessage"] = "Member Deleted Successfully";

            else
                TempData["ErroMessage"] = "Faild to Delet Member";
            return RedirectToAction(nameof(Index));
        }

        #endregion


    
    }
}
