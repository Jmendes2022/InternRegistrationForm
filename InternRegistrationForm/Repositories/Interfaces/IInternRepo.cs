using InternRegistrationForm.Models;

namespace InternRegistrationForm.Repositories
{
    public interface IInternRepo
    {
        Task<InternsModel> AddIntern(InternsModel intern);
        void ArchiveIntern(int id);
        void DropIntern(int id);
        void EditIntern(InternsModel intern);
        Task<List<InternsModel>> GetAllDroppedInterns();
        Task<List<InternsModel>> GetAllInterns();
        Task<InternsModel> GetInternById(int id);
    }
}