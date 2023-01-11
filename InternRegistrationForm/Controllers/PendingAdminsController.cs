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

        //This action adds to the list of pending admins that are waiting to be accepted
        public async Task<IActionResult> AddPendingAdmins(AdminsModel admin)
        {
            ExistingUser existingUser = new ExistingUser(_adminRepo);


            //checking to see if the username from the form already exists
            var alreadyExists = await existingUser.PendingAdminAlreadyExists(admin.Username!);
            if (alreadyExists is true)
            {
                ViewData.Add("alreadyExists", true);

                return RedirectToAction(nameof(AdminController.AddAdmin), NameOfController.ControllerName(nameof(AdminController)));
            }

            //hashing and salting the password to store into the database
            PasswordHasher<dynamic> passwordHasher = new PasswordHasher<dynamic>();
            var hashedPass = passwordHasher.HashPassword(admin.Username!, admin.Password!);
            admin.Password = hashedPass;

            await _adminRepo.AddPendingAdmin(admin);

            
            return RedirectToAction(nameof(PendingAdminAddedView), admin);
        }

        //This action accepts any pending admins that are waiting for approval.
        public async Task<IActionResult> AcceptPendingAdmin(int id)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                //Checks to see if the admin ID is null. if the Id Is null, then redirects back to log in page.
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }
            if (HttpContext.Session.GetInt32("PermissionsLevel") != 2)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            await _adminRepo.AcceptPendingAdminById(id);
            return RedirectToAction(nameof(PendingAdminsListView));
        }

        //This action deletes admins that have submitted an admin account request. If deleted they will have to resubmit the form.
        public IActionResult DeletePendingAdmin(int id)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                //Checks to see if the admin ID is null. if the Id Is null, then redirects back to log in page.
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }
            if (HttpContext.Session.GetInt32("PermissionsLevel") != 2)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            _adminRepo.DeletePendingAdmin(id);
            return RedirectToAction(nameof(PendingAdminsListView));
        }

        //This action displays all the admins that are waiting to get accepted
        public async Task<IActionResult> PendingAdminsListView()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            List<AdminsModel> pendingAdminsList = await _adminRepo.GetAllPendingAdmins();

            return View(pendingAdminsList);
        }


        //This just returns a friendly message to inform the person that they have been added.
        public IActionResult PendingAdminAddedView(AdminsModel admin)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Firstname = admin.FirstName;
                return View();
            }

            return View();
        }
    }
}
