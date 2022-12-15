using Dapper;
using InternRegistrationForm.Models;
using System.Data;
using System.Data.SqlClient;

namespace InternRegistrationForm.Repositories
{
    public class InternRepo : IInternRepo
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _context;

        public InternRepo(IConfiguration config, IHttpContextAccessor context)
        {
            _config = config;
            _context = context;
        }

        public async Task<InternsModel> AddIntern(InternsModel intern)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            intern.Id = await dbConnection.QuerySingleOrDefaultAsync<int>("spIntern_AddIntern", new 
            { 
                FirstName = intern.FirstName, 
                LastName = intern.LastName, 
                Department = intern.Department, 
                DegreePlan = intern.DegreePlan,
                Role = intern.Role,
                StartDate = intern.StartDate,
                EndDate = intern.EndDate,
                DateCreated = intern.DateCreated,
                DegreeConcentration = intern.DegreeConcentration,
                PersonalEmail = intern.PersonalEmail,
                SchoolEmail = intern.SchoolEmail,
                InternshipEmail = intern.InternshipEmail,
                Track = intern.Track,
                InviteToMeetings = intern.InviteToMeetings,
                Email = intern.Email,
                EntranceSurvey = intern.EntranceSurvey,
                WelcomeDocument = intern.WelcomeDocument,
                ThreeSignedDocuments = intern.ThreeSignedDocuments,
                ESETTraining = intern.ESETTraining,
                Resume = intern.Resume,
                Orientation1Access = intern.Orientation1Access,
                Orientation2Access = intern.Orientation2Access,
                TCWAccess = intern.TCWAccess,
                PlannerAccess = intern.PlannerAccess,
                TeamGroupChat = intern.TeamGroupChat,
                OneDriveSetup = intern.OneDriveSetup,
                AzureSetup = intern.AzureSetup,
                ExitSurveySent = intern.ExitSurveySent,
                Masterclass = intern.Masterclass,
                InternNotes = intern.InternNotes,
                LastUpdatedBy = intern.LastUpdatedBy,
                LastUpdate = intern.LastUpdate

            }, commandType: CommandType.StoredProcedure);
            return intern;
        }

        public async void EditIntern(InternsModel intern)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            await dbConnection.QuerySingleOrDefaultAsync<InternsModel>("spIntern_EditIntern", new 
            {
                Id = intern.Id,
                FirstName = intern.FirstName,
                LastName = intern.LastName,
                Department = intern.Department,
                DegreePlan = intern.DegreePlan,
                Role = intern.Role,
                StartDate = intern.StartDate,
                EndDate = intern.EndDate,
                DegreeConcentration = intern.DegreeConcentration,
                PersonalEmail = intern.PersonalEmail,
                SchoolEmail = intern.SchoolEmail,
                InternshipEmail = intern.InternshipEmail,
                Track = intern.Track,
                InviteToMeetings = intern.InviteToMeetings,
                Email = intern.Email,
                EntranceSurvey = intern.EntranceSurvey,
                WelcomeDocument = intern.WelcomeDocument,
                ThreeSignedDocuments = intern.ThreeSignedDocuments,
                ESETTraining = intern.ESETTraining,
                Resume = intern.Resume,
                Orientation1Access = intern.Orientation1Access,
                Orientation2Access = intern.Orientation2Access,
                TCWAccess = intern.TCWAccess,
                PlannerAccess = intern.PlannerAccess,
                TeamGroupChat = intern.TeamGroupChat,
                OneDriveSetup = intern.OneDriveSetup,
                AzureSetup = intern.AzureSetup,
                ExitSurveySent = intern.ExitSurveySent,
                Masterclass = intern.Masterclass,
                InternNotes = intern.InternNotes,
                LastUpdatedBy = intern.LastUpdatedBy,
                LastUpdate = intern.LastUpdate

            }, commandType: CommandType.StoredProcedure);

        }

        public async Task<List<InternsModel>> GetAllDroppedInterns()
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            List<InternsModel> droppedInterns = (await dbConnection.QueryAsync<InternsModel>("spIntern_GetAllDroppedInterns", new { }, commandType: CommandType.StoredProcedure)).ToList();
            return droppedInterns;
        }

        public async void DropIntern(int id)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            await dbConnection.ExecuteAsync("spIntern_DropIntern", new { id = id }, commandType: CommandType.StoredProcedure);
        }

        public async void ArchiveIntern(int id)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            await dbConnection.ExecuteAsync("spIntern_ArchiveIntern", new { id = id }, commandType: CommandType.StoredProcedure);
        }

        public async Task<InternsModel> GetInternById(int id)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            InternsModel intern = await dbConnection.QuerySingleOrDefaultAsync<InternsModel>("spIntern_GetInternById", new { id = id }, commandType: CommandType.StoredProcedure);

            return intern;
        }

        public async Task<List<InternsModel>> GetAllInterns()
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            List<InternsModel> interns = (await dbConnection.QueryAsync<InternsModel>("spIntern_GetAllInterns", new { },  commandType: CommandType.StoredProcedure)).ToList();
            return interns;
        }


    }
}
