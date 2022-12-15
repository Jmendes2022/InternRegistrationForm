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
            return View();
        }

        public async Task<IActionResult> MissingESET()
        {
            List<InternsModel> interns = await _reportRepo.MissingESET();
            
            ReportViewModel report = new ReportViewModel();
            report.Interns = interns;
            report.ReportName = "Missing ESET";

            return View(report);
        }

        public async Task<IActionResult> MissingResume()
        {
            List<InternsModel> interns = await _reportRepo.MissingResume();
            
            ReportViewModel report = new ReportViewModel();
            report.Interns = interns;
            report.ReportName = "Missing Resume";

            return View(report);
        }

        public async Task<IActionResult> MissingSurvey()
        {
            List<InternsModel> interns = await _reportRepo.MissingSurvey();

            ReportViewModel report = new ReportViewModel();
            report.Interns = interns;
            report.ReportName = "Missing Survey";

            
            return View(report);
        }

        public async Task<IActionResult> MissingOrientation()
        {
            List<InternsModel> interns = await _reportRepo.MissingOrientation();

            ReportViewModel report = new ReportViewModel();
            report.Interns = interns;
            report.ReportName = "Missing Orientation";
            
            
            return View(report);
        }

        public async Task<IActionResult> MissingThreeDocuments()
        {
            List<InternsModel> interns = await _reportRepo.MissingThreeDocuments();

            ReportViewModel report = new ReportViewModel();
            report.Interns = interns;
            report.ReportName = "Missing Three Documents";
            
            return View(report);
        }

        public async Task<IActionResult> MissingMasterclass()
        {
            List<InternsModel> interns = await _reportRepo.MissingMasterclass();

            ReportViewModel report = new ReportViewModel();
            report.Interns = interns;
            report.ReportName = "Missing Three Documents";

            return View(report);
        }

    }
}
