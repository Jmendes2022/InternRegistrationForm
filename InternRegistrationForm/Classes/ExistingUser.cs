using InternRegistrationForm.Models;
using InternRegistrationForm.Repositories;
using Microsoft.Extensions.Configuration;

namespace InternRegistrationForm.Classes
{
    public class ExistingUser
    {
        private readonly IAdminRepo _adminRepo;

        public ExistingUser(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public async Task<bool> PendingAdminAlreadyExists(string username)
        {
            List<AdminsModel> allUsers = await _adminRepo.GetAllPendingAdmins();
            var result = await _adminRepo.GetAllAdmins();

            foreach (var item in result)
            {
                allUsers.Add(item);
            }

            foreach (var admin in allUsers)
            {
                if (admin.Username == username)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> UserAlreadyExists(string username)
        {
            List<AdminsModel> allAdmins = await _adminRepo.GetAllAdmins();

            foreach (var admin in allAdmins)
            {
                if (admin.Username == username)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
