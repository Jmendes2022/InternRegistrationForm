using InternRegistrationForm.Classes;
using InternRegistrationForm.Models;
using InternRegistrationForm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InternRegistrationForm.Controllers
{
    public class InternController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInternRepo _internRepo;
        private readonly IAdminRepo _adminRepo;
        private readonly ITermDatesRepo _termDates;

        public InternController(ILogger<HomeController> logger, IInternRepo internRepo, IAdminRepo adminRepo, ITermDatesRepo termDates)
        {
            _logger = logger;
            _internRepo = internRepo;
            _adminRepo = adminRepo;
            _termDates = termDates;
        }


        //This action registers interns to the database
        [HttpPost]
        public async Task<IActionResult> Registration(InternsModel intern)
        {
            //if the current user has 0 permissions, they dont have ability to edit anything.

            if (HttpContext.Session.GetInt32("PermissionsLevel") == 0)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            intern.LastUpdatedBy = HttpContext.Session.GetString("AdminFirstName") + " " + HttpContext.Session.GetString("AdminLastName");
            intern.DateCreated = DateTime.Now;
            intern.LastUpdate = DateTime.Now;

            await _internRepo.AddIntern(intern);

            return RedirectToAction(nameof(AdminController.AdminIndex), NameOfController.ControllerName(nameof(AdminController)));
        }


        //This action gets the form page to register interns
        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            if (HttpContext.Session.GetInt32("PermissionsLevel") == 0)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            RegistrationViewModel registrationViewModel = new RegistrationViewModel();
            registrationViewModel.TermDates = await _termDates.GetAll();

            return View(nameof(Registration), registrationViewModel);
        }

        //Returns an intern from the dropped data tables
        public IActionResult ResurrectIntern(int id)
        {
            if (HttpContext.Session.GetInt32("PermissionsLevel") == 0)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            _internRepo.ResurrectIntern(id);

            return RedirectToAction(nameof(AdminController.AdminIndex), NameOfController.ControllerName(nameof(AdminController)));
        }

        //This action displays an individual dropped intern
        public async Task<IActionResult> DisplayDroppedIntern(int id)
        {
            InternsModel droppedIntern = await _internRepo.GetDroppedInternById(id);

            return View(droppedIntern);
        }

        //This action displays the list of all the dropped interns
        public async Task<IActionResult> DroppedInterns()
        {

            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            List<InternsModel> droppedInterns = await _internRepo.GetAllDroppedInterns();

            return View(droppedInterns);
        }

        //This action drops an intern from the intern data table and stores them into a droppedInterns table
        public IActionResult DropIntern(int internId)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            if (HttpContext.Session.GetInt32("PermissionsLevel") == 0)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            _internRepo.DropIntern(internId);

            return RedirectToAction(nameof(AdminController.AdminIndex), NameOfController.ControllerName(nameof(AdminController)));
        }

        //This action allows for an itern to be edited
        [HttpPost]
        public IActionResult EditIntern(InternsModel intern)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            if (HttpContext.Session.GetInt32("PermissionsLevel") == 0)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            intern.LastUpdatedBy = HttpContext.Session.GetString("AdminFirstName") + " " + HttpContext.Session.GetString("AdminLastName");
            intern.LastUpdate = DateTime.Now;


            _internRepo.EditIntern(intern);

            return RedirectToAction(nameof(AdminController.AdminIndex), NameOfController.ControllerName(nameof(AdminController)));
        }

        //This gets the intern intial information ready to be edited in the view
        [HttpGet]
        public async Task<IActionResult> EditIntern(int internId)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }
            if (HttpContext.Session.GetInt32("PermissionsLevel") == 0)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            InternsModel intern = await _internRepo.GetInternById(internId);

            return View(intern);
        }

        //This action displays an individual intern 
        public async Task<IActionResult> DisplayIntern(int internId)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            InternsModel intern = await _internRepo.GetInternById(internId);
            if (intern.InternNotes == null)
            {
                intern.InternNotes = "There are no notes currently";
            }

            return View(intern);
        }

        //This action archives an intern after they have graduated.
        public IActionResult ArchiveIntern(int internId)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            if (HttpContext.Session.GetInt32("PermissionsLevel") == 0)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            _internRepo.ArchiveIntern(internId);

            return RedirectToAction(nameof(AdminController.AdminIndex), NameOfController.ControllerName(nameof(AdminController)));
        }

        public async Task<IActionResult> DisplayArchivedIntern(int internId)
        {
            var archivedIntern = await _adminRepo.GetArchivedInternById(internId);

            return View(archivedIntern);
        }

        public IActionResult ResurrectArchivedIntern(int id)
        {
            if (HttpContext.Session.GetInt32("PermissionsLevel") == 0)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            _adminRepo.ResurrectArchivedIntern(id);

            return RedirectToAction(nameof(AdminController.AdminIndex), NameOfController.ControllerName(nameof(AdminController)));
        }

        public async Task<IActionResult> ArchivedInternsList()
        {

            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            List<InternsModel> archivedInterns = await _adminRepo.GetAllArchivedInterns();

            return View(archivedInterns);
        }
    }
}
