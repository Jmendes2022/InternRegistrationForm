using InternRegistrationForm.Classes;
using InternRegistrationForm.Models;
using InternRegistrationForm.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternRegistrationForm.Controllers
{
    public class PendingAdminsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInternRepo _internRepo;
        private readonly IAdminRepo _adminRepo;

        public PendingAdminsController(ILogger<HomeController> logger, IInternRepo internRepo, IAdminRepo adminRepo)
        {
            _logger = logger;
            _internRepo = internRepo;
            _adminRepo = adminRepo;
        }

        public async Task<IActionResult> AddPendingAdmins(AdminsModel admin)
        {
            ExistingUser existingUser = new ExistingUser(_adminRepo);

            var alreadyExists = await existingUser.PendingAdminAlreadyExists(admin.Username!);
            if (alreadyExists is true)
            {
                ViewData.Add("alreadyExists", true);

                return RedirectToAction(nameof(AdminController.AddAdmin), NameOfController.ControllerName(nameof(AdminController)));
            }

            PasswordHasher<dynamic> passwordHasher = new PasswordHasher<dynamic>();
            var hashedPass = passwordHasher.HashPassword(admin.Username!, admin.Password!);
            admin.Password = hashedPass;

            await _adminRepo.AddPendingAdmin(admin);

            
            return RedirectToAction(nameof(PendingAdminAddedView), admin);
        }

        public async Task<IActionResult> AcceptPendingAdmin(int id)
        {
            await _adminRepo.AcceptPendingAdminById(id);
            return RedirectToAction(nameof(PendingAdminsListView));
        }

        public IActionResult DeletePendingAdmin(int id)
        {
            _adminRepo.DeletePendingAdmin(id);
            return RedirectToAction(nameof(PendingAdminsListView));
        }

        public async Task<IActionResult> PendingAdminsListView()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            List<AdminsModel> pendingAdminsList = await _adminRepo.GetAllPendingAdmins();

            return View(pendingAdminsList);
        }

        public IActionResult PendingAdminAddedView(AdminsModel admin)
        {
            ViewBag.Firstname = admin.FirstName;
            return View();
        }
    }
}
