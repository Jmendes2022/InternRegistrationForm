using InternRegistrationForm.Models;

namespace InternRegistrationForm.Repositories
{
    public interface IReportRepo
    {
        Task<List<InternsModel>> MissingESET();
        Task<List<InternsModel>> MissingMasterclass();
        Task<List<InternsModel>> MissingOrientation();
        Task<List<InternsModel>> MissingResume();
        Task<List<InternsModel>> MissingSurvey();
        Task<List<InternsModel>> MissingThreeDocuments();
    }
}