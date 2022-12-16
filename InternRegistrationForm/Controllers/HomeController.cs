using InternRegistrationForm.Classes;
using InternRegistrationForm.Models;
using InternRegistrationForm.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InternRegistrationForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IInternRepo _internRepo;
        private readonly IAdminRepo _adminRepo;
        private readonly ITermDatesRepo _termDatesRepo;

        public HomeController(ILogger<HomeController> logger, IInternRepo internRepo, IAdminRepo adminRepo, ITermDatesRepo termDatesRepo)
        {
            _logger = logger;
            _internRepo = internRepo;
            _adminRepo = adminRepo;
            _termDatesRepo = termDatesRepo;
        }

        public async Task<IActionResult> TermDates()
        {
            List<TermDatesModel> termDates = await _termDatesRepo.GetAll();

            return View(termDates);
        }

        public IActionResult ResurrectIntern(int id)
        {
             _internRepo.ResurrectIntern(id);

            return RedirectToAction(nameof(AdminIndex));
        }

        public async Task<IActionResult> DisplayDroppedIntern(int id)
        { 
            InternsModel droppedIntern = await _internRepo.GetDroppedInternById(id);

            return View(droppedIntern);
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


        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public async Task<IActionResult> AdminIndex()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
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
                return RedirectToAction(nameof(LogIn));
            }

            return RedirectToAction(nameof(DisplayIntern), intern);
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }

            return RedirectToAction(nameof(LogIn));
        }

        
        public async Task<IActionResult> LogIn(LogInViewModel logInModel)
        {
            AdminsModel admin = await _adminRepo.GetAdminByUsername(logInModel.adminsModel.Username);
            logInModel.AllAdmins = await _adminRepo.GetAllAdmins();

            PasswordHasher<AdminsModel> passwordHasher = new PasswordHasher<AdminsModel>();
            try
            {
                var result = passwordHasher.VerifyHashedPassword(admin, admin.Password, logInModel.adminsModel.Password);
                
                if (result != PasswordVerificationResult.Success)
                {
                    logInModel.LoggedAttemptFailed = true;
                    return View(logInModel);
                }
                else
                {
                   
                        HttpContext.Session.SetString("AdminFirstName", admin.FirstName);
                        HttpContext.Session.SetString("AdminLastName", admin.LastName);
                        HttpContext.Session.SetInt32("AdminId", admin.Id);
                        HttpContext.Session.SetString("HasPermissions", admin.HasPermissions.ToString());

                    logInModel.LoggedAttemptFailed = false;
                    return RedirectToAction(nameof(AdminIndex));
                }
            }
            catch (Exception)
            {
                logInModel.LoggedAttemptFailed = true;
                return View(logInModel);
            }

        }

        [HttpGet]
        public async Task<IActionResult> LogIn()
        {
            LogInViewModel logInModel = new LogInViewModel() { LoggedAttemptFailed = false };
            logInModel.AllAdmins = (await _adminRepo.GetAllAdmins()).ToList();
            return View(logInModel);
        }

        public IActionResult DropIntern(int internId)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }

            _internRepo.DropIntern(internId);

            return RedirectToAction(nameof(AdminIndex));
        }

        public async Task<IActionResult> DisplayArchivedIntern(int internId)
        {
            var archivedIntern = await _adminRepo.GetArchivedInternById(internId);

            return View(archivedIntern);
        }

        public  IActionResult ResurrectArchivedIntern(int id)
        {
            _adminRepo.ResurrectArchivedIntern(id);

            return RedirectToAction(nameof(AdminIndex));
        }

        public IActionResult ArchiveIntern(int internId)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }

            _internRepo.ArchiveIntern(internId);

            return RedirectToAction(nameof(AdminIndex));
        }

        public async Task<IActionResult> ArchivedInternsList()
        {

            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }

            List<InternsModel> archivedInterns = await _adminRepo.GetAllArchivedInterns();
            

            return View(archivedInterns);
        }


        [HttpPost]
        public IActionResult EditIntern(InternsModel intern)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }
            intern.LastUpdatedBy = HttpContext.Session.GetString("AdminFirstName") + " " + HttpContext.Session.GetString("AdminLastName");
            intern.LastUpdate = DateTime.Now;


            _internRepo.EditIntern(intern);

            return RedirectToAction(nameof(AdminIndex));
        }
        [HttpGet]
        public async Task<IActionResult> EditIntern(int internId)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }
            

            InternsModel intern = await _internRepo.GetInternById(internId);

            return View(intern);
        }

        [HttpGet]
        public async Task<IActionResult> EditAdmin(int id)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }

            AdminsModel admin = await _adminRepo.GetAdminById(id);

            return View(admin);
        }

        [HttpPost]
        public async Task<IActionResult> EditAdmin(AdminsModel admin)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }

            AdminsModel updatedAdmin = await _adminRepo.EditAdmin(admin);
            return RedirectToAction(nameof(AdminsList));
        }

        [HttpPost]
        public async Task<IActionResult> Registration(InternsModel intern)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }

            intern.LastUpdatedBy = HttpContext.Session.GetString("AdminFirstName") + " " + HttpContext.Session.GetString("AdminLastName");
            intern.DateCreated = DateTime.Now;
            intern.LastUpdate = DateTime.Now;

            await _internRepo.AddIntern(intern);

            if (HttpContext.Session.GetInt32("AdminId") != null)
            {
                return RedirectToAction(nameof(AdminIndex));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Registration()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }
            return View(nameof(Registration));
        }


        public async Task<IActionResult> DisplayIntern(int internId)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }

            InternsModel intern = await _internRepo.GetInternById(internId);
            if (intern.InternNotes == null)
            {
                intern.InternNotes = "There are no notes currently";
            }

            return View(intern);
        }
        

        public async Task<IActionResult> AdminsList()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }

            List<AdminsModel> admins = await _adminRepo.GetAllAdmins();


            return View(admins);
        }

        public async Task<IActionResult> DropAdmin(int id)
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }

            AdminsModel admin = await _adminRepo.GetAdminById(id);
            _adminRepo.DropAdmin(admin);

            return RedirectToAction(nameof(AdminsList));
        }

        public async Task<IActionResult> AdminRegistration()
        {
            List<AdminsModel> allAdmins = await _adminRepo.GetAllAdmins();


            return View(nameof(AddAdmin));
        }

        public async Task<IActionResult> AddPendingAdmins(AdminsModel admin)
        {
            ExistingUser existingUser = new ExistingUser(_adminRepo);

            var alreadyExists = await existingUser.PendingAdminAlreadyExists(admin.Username);
            if (alreadyExists is true)
            {
                ViewData.Add("alreadyExists", true);
                return View(nameof(AddAdmin));
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
                return RedirectToAction(nameof(LogIn));
            }

            List<AdminsModel> pendingAdminsList = await _adminRepo.GetAllPendingAdmins();
            
            return View(pendingAdminsList);
        }

        public IActionResult PendingAdminAddedView(AdminsModel admin)
        {
            ViewBag.Firstname = admin.FirstName;
            return View();
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

            await _adminRepo.AddAdmin(admin);

            return RedirectToAction(nameof(AdminsList));
        }

        public async Task<IActionResult> DroppedInterns()
        {
            if (HttpContext.Session.GetInt32("AdminId") == null)
            {
                return RedirectToAction(nameof(LogIn));
            }

            List<InternsModel> droppedInterns = await _internRepo.GetAllDroppedInterns();

            return View(droppedInterns);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}