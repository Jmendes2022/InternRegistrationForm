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
            List<TermDatesModel> termDates = await _termDatesRepo.GetAll();

            return View(termDates);
        }

        public IActionResult DeleteTerm(int id)
        {
            _termDatesRepo.DeleteTerm(id);
            return RedirectToAction(nameof(TermDates));
        }

        [HttpGet]
        public IActionResult AddTerm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTerm(TermDatesModel term)
        {
            await _termDatesRepo.AddTerm(term);
            return RedirectToAction(nameof(TermDates));
        }

        public IActionResult EditSingleTerm(TermDatesModel term)
        {
            _termDatesRepo.EditDateForTrack(term);
            return RedirectToAction(nameof(TermDates));
        }

        public IActionResult EditSingleTermDate(int id)
        {
            TermDatesModel termDatesModel = new TermDatesModel();
            termDatesModel.Id = id;

            return View(termDatesModel);
        }
    }
}
