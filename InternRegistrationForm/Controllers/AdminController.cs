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


        //This action just renders the view for Adding in a master admin account
        [HttpGet]
        public async Task<IActionResult> AddMasterAdmin()
        {
            List<AdminsModel> admins = await _adminRepo.GetAllAdmins();

            //This counts all the admins in the database and redirects to the log in screen if there are 1.
            //This prevents users from being able to gain access to the action unnecessarily.
            if (admins.Count() > 0)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            return View();
        }


        //This action adds in a Master admin in the event that there are no other admins present in the database
        //Due to the log-in & verify feature, this master login is added.
        [HttpPost]
        public async Task<IActionResult> AddMasterAdmin(AdminsModel admin)
        { 
            admin.PermissionsLevel = 2;

            PasswordHasher<dynamic> passwordHasher = new PasswordHasher<dynamic>();
            var hashedPass = passwordHasher.HashPassword(admin.Username!, admin.Password!);
            admin.Password = hashedPass;

            await _adminRepo.AddAdmin(admin);

            return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
        }


        //The index page after logging in.
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


        //This action redirects to the action to display an intern after an Intern is selected.
        [HttpPost]
        public IActionResult AdminIndex(InternsModel intern)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction("LogIn", nameof(HomeController));
            }

            return RedirectToAction(nameof(InternController.DisplayIntern), NameOfController.ControllerName(nameof(InternController)));
        }


        //This returns the view to register a new admin
        public IActionResult AdminRegistration()
        {
            return View(nameof(AddAdmin));
        }


        //This returns the view of an added admin
        public async Task<IActionResult> AddAdmin(AdminsModel admin)
        {
            PasswordHasher hasher = new PasswordHasher(admin.Username!, admin.Password!);
            admin.Password = hasher.Pass;

            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                    //This returns back to the log in back since this action is shared with the regular admin registration.
                    //This code executes if adding an admin from the log in page. They get stored as a "pending admin".
                    await _adminRepo.AddPendingAdmin(admin);
                    return RedirectToAction(nameof(PendingAdminsController.PendingAdminAddedView), NameOfController.ControllerName(nameof(PendingAdminsController)));
            }
            else if (HttpContext.Session.GetInt32("PermissionsLevel") != 2)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }
           
            //adds the admin to the database if an admin is logged in, then returns that view with the admins list. 
            await _adminRepo.AddAdmin(admin);
            return RedirectToAction(nameof(AdminsList));
        }


        //This action gets an admin to edit.
        [HttpGet]
        public async Task<IActionResult> EditAdmin(int id)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                //Checks to see if the admin ID is null. if the Id Is null, then redirects back to log in page.
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            //This redirects if the permissions level is not high enough.
            if (HttpContext.Session.GetInt32("PermissionsLevel") != 2)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            //gets admin by the ID.
            AdminsModel admin = await _adminRepo.GetAdminById(id);

            //Returns the view with the current admin selected, allowing for editing input fields.
            return View(admin);
        }

        //This action is called after the admin is edited.
        [HttpPost]
        public async Task<IActionResult> EditAdmin(AdminsModel admin)
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

            PasswordHasher hasher = new PasswordHasher(admin.Username!, admin.Password!);
            admin.Password = hasher.Pass;

            //Replaces the previous admin in the database with the admin that was passed into the action.
            AdminsModel updatedAdmin = await _adminRepo.EditAdmin(admin);
            //returns the view of the admins list, containing the newly updated admin.
            return RedirectToAction(nameof(AdminsList));
        }

        //This just displays a view of all the current admins within the database
        public async Task<IActionResult> AdminsList()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                //Checks to see if the admin ID is null. if the Id Is null, then redirects back to log in page.
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            //Gets all the admins from the database
            List<AdminsModel> admins = await _adminRepo.GetAllAdmins();

            //returns the view with the model object of List<admins>
            return View(admins);
        }

        //This action takes an admin and drops them from the Admins table
        public async Task<IActionResult> DropAdmin(int id)
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

            //Gets the admin by the ID
            AdminsModel admin = await _adminRepo.GetAdminById(id);
            //Deletes the admin from the Admins table and adds them into the DroppedAdmins table
            _adminRepo.DropAdmin(admin);

            //returns the new list of admins
            return RedirectToAction(nameof(AdminsList));
        }

    }
}
