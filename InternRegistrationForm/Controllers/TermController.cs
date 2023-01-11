using InternRegistrationForm.Classes;
using InternRegistrationForm.Models;
using InternRegistrationForm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InternRegistrationForm.Controllers
{
    public class TermController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITermDatesRepo _termDatesRepo;

        public TermController(ILogger<HomeController> logger, ITermDatesRepo termDatesRepo)
        {
            _logger = logger;
            _termDatesRepo = termDatesRepo;
        }

        public async Task<IActionResult> TermDates()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }

            List<TermDatesModel> termDates = await _termDatesRepo.GetAll();

            return View(termDates);
        }

        public IActionResult DeleteTerm(int id)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }
            if (HttpContext.Session.GetInt32("PermissionsLevel") == 0)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }


            _termDatesRepo.DeleteTerm(id);
            return RedirectToAction(nameof(TermDates));
        }

        [HttpGet]
        public IActionResult AddTerm()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }

            if (HttpContext.Session.GetInt32("PermissionsLevel") == 0)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTerm(TermDatesModel term)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }

            if (HttpContext.Session.GetInt32("PermissionsLevel") == 0)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            await _termDatesRepo.AddTerm(term);
            return RedirectToAction(nameof(TermDates));
        }

        public IActionResult EditSingleTerm(TermDatesModel term)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }

            if (HttpContext.Session.GetInt32("PermissionsLevel") == 0)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            _termDatesRepo.EditDateForTrack(term);
            return RedirectToAction(nameof(TermDates));
        }

        public IActionResult EditSingleTermDate(int id)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }

            if (HttpContext.Session.GetInt32("PermissionsLevel") == 0)
            {
                return RedirectToAction(nameof(HomeController.InvalidPermissions), NameOfController.ControllerName(nameof(HomeController)));
            }

            TermDatesModel termDatesModel = new TermDatesModel();
            termDatesModel.Id = id;

            return View(termDatesModel);
        }
    }
}
