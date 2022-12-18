using InternRegistrationForm.Classes;
using InternRegistrationForm.Models;
using InternRegistrationForm.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternRegistrationForm.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInternRepo _internRepo;
        private readonly IAdminRepo _adminRepo;

        public AdminController(ILogger<HomeController> logger, IInternRepo internRepo, IAdminRepo adminRepo)
        {
            _logger = logger;
            _internRepo = internRepo;
            _adminRepo = adminRepo;
        }

        [HttpGet]
        public IActionResult AddMasterAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMasterAdmin(AdminsModel admin)
        { 
            admin.HasPermissions = true;

            PasswordHasher<dynamic> passwordHasher = new PasswordHasher<dynamic>();
            var hashedPass = passwordHasher.HashPassword(admin.Username!, admin.Password!);
            admin.Password = hashedPass;

            await _adminRepo.AddAdmin(admin);

            return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
        }

        public async Task<IActionResult> AdminIndex()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }
            AdminIndexViewModel viewModel = new AdminIndexViewModel();

            viewModel.interns = await _internRepo.GetAllInterns();
            viewModel.PendingAdminsList = await _adminRepo.GetAllPendingAdmins();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AdminIndex(InternsModel intern)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction("LogIn", nameof(HomeController));
            }

            return RedirectToAction(nameof(InternController.DisplayIntern), NameOfController.ControllerName(nameof(InternController)));
        }

        public IActionResult AdminRegistration()
        {
            return View(nameof(AddAdmin));
        }

        public async Task<IActionResult> AddAdmin(AdminsModel admin)
        {
            ExistingUser existingUser = new ExistingUser(_adminRepo);

            var alreadyExists = await existingUser.UserAlreadyExists(admin.Username);
            if (alreadyExists is true)
            {
                ViewData.Add("alreadyExists", true);
                return View();
            }

            PasswordHasher<dynamic> passwordHasher = new PasswordHasher<dynamic>();
            var hashedPass = passwordHasher.HashPassword(admin.Username!, admin.Password!);
            admin.Password = hashedPass;

            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                await _adminRepo.AddPendingAdmin(admin);
                return RedirectToAction(nameof(PendingAdminsController.PendingAdminAddedView), NameOfController.ControllerName(nameof(PendingAdminsController)));
            }

            await _adminRepo.AddAdmin(admin);
            return RedirectToAction(nameof(AdminsList));
        }

        [HttpGet]
        public async Task<IActionResult> EditAdmin(int id)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {

                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
                ;
            }

            AdminsModel admin = await _adminRepo.GetAdminById(id);

            return View(admin);
        }

        [HttpPost]
        public async Task<IActionResult> EditAdmin(AdminsModel admin)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            AdminsModel updatedAdmin = await _adminRepo.EditAdmin(admin);
            return RedirectToAction(nameof(AdminsList));
        }

        public async Task<IActionResult> AdminsList()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            List<AdminsModel> admins = await _adminRepo.GetAllAdmins();


            return View(admins);
        }

        public async Task<IActionResult> DropAdmin(int id)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            AdminsModel admin = await _adminRepo.GetAdminById(id);
            _adminRepo.DropAdmin(admin);

            return RedirectToAction(nameof(AdminsList));
        }

    }
}
