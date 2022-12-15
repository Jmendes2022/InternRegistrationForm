using Dapper;
using InternRegistrationForm.Models;
using System.Data;
using System.Data.SqlClient;

namespace InternRegistrationForm.Repositories
{
    public class TermDatesRepo : ITermDatesRepo
    {
        private readonly IConfiguration _config;

        public TermDatesRepo(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<TermDatesModel>> GetAll()
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            List<TermDatesModel> termDates = (await dbConnection.QueryAsync<TermDatesModel>("spTermDates_GetAll", new { }, commandType: CommandType.StoredProcedure)).ToList();
            return termDates;
        }

        public async Task<TermDatesModel> GetById(int id)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            TermDatesModel termDate = await dbConnection.QuerySingleOrDefaultAsync<TermDatesModel>("spTermDates_GetById", new {id = id }, commandType: CommandType.StoredProcedure);
            return termDate;
        }

        public async Task<TermDatesModel> AddTerm(TermDatesModel term)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            term.Id = await dbConnection.QuerySingleOrDefaultAsync<int>("spTermDates_AddTerm", new { StartDate = term.StartDate, EndDate = term.EndDate, Track = term.Track }, commandType: CommandType.StoredProcedure);

            return term;
        }

        public void DeleteTerm(int id)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            dbConnection.ExecuteAsync("spTermDates_DeleteTerm", new { }, commandType: CommandType.StoredProcedure);
        }

        public async void EditDateForTrack(TermDatesModel term)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            await dbConnection.QuerySingleOrDefaultAsync<TermDatesModel>("spTermDates_EditDateForTrack", new { id = term.Id, Track = term.Track, StartDate = term.StartDate, EndDate = term.EndDate }, commandType: CommandType.StoredProcedure);
        }


    }
}
