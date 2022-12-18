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

        

        public IActionResult Index()
        {
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
                        
                    return RedirectToAction(nameof(AdminController.AdminIndex), NameOfController.ControllerName(nameof(AdminController)));
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


        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return View();
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