using InternRegistrationForm.Models;
using InternRegistrationForm.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InternRegistrationForm.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportRepo _reportRepo;

        public ReportController(IReportRepo reportRepo)
        {
            _reportRepo = reportRepo;
        }


        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }

            return View();
        }

        public async Task<IActionResult> MissingESET()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }

            List<InternsModel> interns = await _reportRepo.MissingESET();
            
            ReportViewModel report = new ReportViewModel();
            report.Interns = interns;
            report.ReportName = "Missing ESET";

            return View(report);
        }

        public async Task<IActionResult> MissingResume()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }

            List<InternsModel> interns = await _reportRepo.MissingResume();
            
            ReportViewModel report = new ReportViewModel();
            report.Interns = interns;
            report.ReportName = "Missing Resume";

            return View(report);
        }

        public async Task<IActionResult> MissingSurvey()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }

            List<InternsModel> interns = await _reportRepo.MissingSurvey();

            ReportViewModel report = new ReportViewModel();
            report.Interns = interns;
            report.ReportName = "Missing Survey";

            
            return View(report);
        }

        public async Task<IActionResult> MissingOrientation()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }

            List<InternsModel> interns = await _reportRepo.MissingOrientation();

            ReportViewModel report = new ReportViewModel();
            report.Interns = interns;
            report.ReportName = "Missing Orientation";
            
            
            return View(report);
        }

        public async Task<IActionResult> MissingThreeDocuments()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }

            List<InternsModel> interns = await _reportRepo.MissingThreeDocuments();

            ReportViewModel report = new ReportViewModel();
            report.Interns = interns;
            report.ReportName = "Missing Three Documents";
            
            return View(report);
        }

        public async Task<IActionResult> MissingMasterclass()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToRoute(nameof(HomeController), nameof(HomeController.LogIn));
            }

            List<InternsModel> interns = await _reportRepo.MissingMasterclass();

            ReportViewModel report = new ReportViewModel();
            report.Interns = interns;
            report.ReportName = "Missing Master Class";

            return View(report);
        }

    }
}
