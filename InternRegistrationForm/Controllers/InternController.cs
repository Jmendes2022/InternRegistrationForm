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

        public InternController(ILogger<HomeController> logger, IInternRepo internRepo, IAdminRepo adminRepo)
        {
            _logger = logger;
            _internRepo = internRepo;
            _adminRepo = adminRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Registration(InternsModel intern)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            intern.LastUpdatedBy = HttpContext.Session.GetString("AdminFirstName") + " " + HttpContext.Session.GetString("AdminLastName");
            intern.DateCreated = DateTime.Now;
            intern.LastUpdate = DateTime.Now;

            await _internRepo.AddIntern(intern);

            if (HttpContext.Session.GetInt32("AdminId") != null)
            {
                return RedirectToAction(nameof(AdminController.AdminIndex), NameOfController.ControllerName(nameof(AdminController)));
            }

            return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
        }

       
        [HttpGet]
        public IActionResult Registration()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }
            return View(nameof(Registration));
        }


        public IActionResult ResurrectIntern(int id)
        {
            _internRepo.ResurrectIntern(id);

            return RedirectToAction(nameof(AdminController.AdminIndex), NameOfController.ControllerName(nameof(AdminController)));
        }

        public async Task<IActionResult> DisplayDroppedIntern(int id)
        {
            InternsModel droppedIntern = await _internRepo.GetDroppedInternById(id);

            return View(droppedIntern);
        }

        public async Task<IActionResult> DroppedInterns()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            List<InternsModel> droppedInterns = await _internRepo.GetAllDroppedInterns();

            return View(droppedInterns);
        }

        public IActionResult DropIntern(int internId)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            _internRepo.DropIntern(internId);

            return RedirectToAction(nameof(AdminController.AdminIndex), NameOfController.ControllerName(nameof(AdminController)));
        }

        [HttpPost]
        public IActionResult EditIntern(InternsModel intern)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }
            intern.LastUpdatedBy = HttpContext.Session.GetString("AdminFirstName") + " " + HttpContext.Session.GetString("AdminLastName");
            intern.LastUpdate = DateTime.Now;


            _internRepo.EditIntern(intern);

            return RedirectToAction(nameof(AdminController.AdminIndex), NameOfController.ControllerName(nameof(AdminController)));
        }
        [HttpGet]
        public async Task<IActionResult> EditIntern(int internId)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
            }

            InternsModel intern = await _internRepo.GetInternById(internId);

            return View(intern);
        }

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

        public IActionResult ArchiveIntern(int internId)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(HomeController.LogIn), NameOfController.ControllerName(nameof(HomeController)));
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
