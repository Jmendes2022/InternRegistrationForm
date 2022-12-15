using Dapper;
using InternRegistrationForm.Models;
using System.Data;
using System.Data.SqlClient;

namespace InternRegistrationForm.Repositories
{
    public class ReportRepo : IReportRepo
    {
        private readonly IConfiguration _config;

        public ReportRepo(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<InternsModel>> MissingESET()
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            List<InternsModel> interns = (await dbConnection.QueryAsync<InternsModel>("spReport_MissingESET", new { }, commandType: CommandType.StoredProcedure)).ToList();

            return interns;
        }

        public async Task<List<InternsModel>> MissingResume()
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            List<InternsModel> interns = (await dbConnection.QueryAsync<InternsModel>("spReport_MissingResume", new { }, commandType: CommandType.StoredProcedure)).ToList();

            return interns;
        }

        public async Task<List<InternsModel>> MissingSurvey()
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            List<InternsModel> interns = (await dbConnection.QueryAsync<InternsModel>("spReport_MissingSurvey", new { }, commandType: CommandType.StoredProcedure)).ToList();

            return interns;
        }

        public async Task<List<InternsModel>> MissingOrientation()
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            List<InternsModel> interns = (await dbConnection.QueryAsync<InternsModel>("spReport_MissingOrientation", new { }, commandType: CommandType.StoredProcedure)).ToList();

            return interns;
        }
        public async Task<List<InternsModel>> MissingThreeDocuments()
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            List<InternsModel> interns = (await dbConnection.QueryAsync<InternsModel>("spReport_MissingThreeDocuments", new { }, commandType: CommandType.StoredProcedure)).ToList();

            return interns;
        }
        public async Task<List<InternsModel>> MissingMasterclass()
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            List<InternsModel> interns = (await dbConnection.QueryAsync<InternsModel>("spReport_MissingMasterclass", new { }, commandType: CommandType.StoredProcedure)).ToList();

            return interns;
        }
    }
}
