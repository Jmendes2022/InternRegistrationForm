using InternRegistrationForm.Models;

namespace InternRegistrationForm.Repositories
{
    public interface IAdminRepo
    {
        Task<AdminsModel> AcceptPendingAdminById(int id);
        Task<AdminsModel> AddAdmin(AdminsModel admin);
        Task<AdminsModel> AddPendingAdmin(AdminsModel admin);
        void DeletePendingAdmin(int id);
        void DropAdmin(AdminsModel admin);
        Task<AdminsModel> EditAdmin(AdminsModel admin);
        Task<AdminsModel> GetAdmin(string username, string password);
        Task<AdminsModel> GetAdminById(int id);
        Task<AdminsModel> GetAdminByUsername(string username);
        Task<List<AdminsModel>> GetAllAdmins();
        Task<List<InternsModel>> GetAllArchivedInterns();
        Task<List<AdminsModel>> GetAllPendingAdmins();
    }
}