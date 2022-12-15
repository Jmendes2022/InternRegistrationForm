using InternRegistrationForm.Models;

namespace InternRegistrationForm.Repositories
{
    public interface ITermDatesRepo
    {
        Task<TermDatesModel> AddTerm(TermDatesModel term);
        void DeleteTerm(int id);
        void EditDateForTrack(TermDatesModel term);
        Task<List<TermDatesModel>> GetAll();
        Task<TermDatesModel> GetById(int id);
    }
}