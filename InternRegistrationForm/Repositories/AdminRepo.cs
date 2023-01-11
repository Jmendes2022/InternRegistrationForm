using Dapper;
using InternRegistrationForm.Models;
using System.Data;
using System.Data.SqlClient;

namespace InternRegistrationForm.Repositories
{
    public class AdminRepo : IAdminRepo
    {
        private readonly IConfiguration _config;

        public AdminRepo(IConfiguration config)
        {
            _config = config;
        }

        public async Task<AdminsModel> AddAdmin(AdminsModel admin)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            admin.Id = await dbConnection.QuerySingleOrDefaultAsync<int>("spAdmin_AddAdmin", new { Firstname = admin.FirstName, LastName = admin.LastName, Department = admin.Department, Username = admin.Username, Password = admin.Password, InternshipEmail = admin.InternshipEmail, SchoolEmail = admin.SchoolEmail, PersonalEmail = admin.PersonalEmail, PermissionsLevel = admin.PermissionsLevel}, commandType: CommandType.StoredProcedure);

            return admin;
        }

        public async Task<AdminsModel> GetAdminByUsername(string username)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            AdminsModel admin = await dbConnection.QuerySingleOrDefaultAsync<AdminsModel>("spAdmin_GetAdminByUsername", new { username = username }, commandType: CommandType.StoredProcedure);

            return admin;
        }

        public async void DeletePendingAdmin(int id)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            await dbConnection.ExecuteAsync("spAdmin_DeletePendingAdmin", new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<AdminsModel>> GetAllPendingAdmins()
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            List<AdminsModel> pendingAdmins = (await dbConnection.QueryAsync<AdminsModel>("spAdmin_GetAllPendingAdmins", new { }, commandType: CommandType.StoredProcedure)).ToList();

            return pendingAdmins;
        }

        public async Task<AdminsModel> AddPendingAdmin(AdminsModel admin)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            admin.Id = await dbConnection.QuerySingleOrDefaultAsync<int>("spAdmin_AddPendingAdmin", new { FirstName = admin.FirstName, LastName = admin.LastName, Department = admin.Department, Username = admin.Username, Password = admin.Password, InternshipEmail = admin.InternshipEmail, SchoolEmail = admin.SchoolEmail, PersonalEmail = admin.PersonalEmail, PermissionsLevel = admin.PermissionsLevel }, commandType: CommandType.StoredProcedure);

            return admin;
        }

        public async Task<AdminsModel> AcceptPendingAdminById(int id)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            AdminsModel admin =  await dbConnection.QuerySingleOrDefaultAsync<AdminsModel>("spAdmin_AcceptPendingAdminById", new { Id = id }, commandType: CommandType.StoredProcedure);
            return admin;
        }

        public async Task<AdminsModel> GetAdmin(string username, string password)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            AdminsModel admin = await dbConnection.QuerySingleOrDefaultAsync<AdminsModel>("spAdmin_GetAdmin", new { username = username, password = password }, commandType: CommandType.StoredProcedure);

            return admin;
        }

        public async Task<AdminsModel> GetAdminById(int id)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            AdminsModel admin = await dbConnection.QuerySingleOrDefaultAsync<AdminsModel>("spAdmin_GetAdminById", new { id = id }, commandType: CommandType.StoredProcedure);

            return admin;
        }


        public async Task<AdminsModel> EditAdmin(AdminsModel admin)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            admin = await dbConnection.QuerySingleOrDefaultAsync<AdminsModel>("spAdmin_EditAdmin", new
            {
                Id = admin.Id,
                Firstname = admin.FirstName,
                Lastname = admin.LastName,
                Department = admin.Department,
                Username = admin.Username,
                Password = admin.Password,
                InternshipEmail = admin.InternshipEmail,
                SchoolEmail = admin.SchoolEmail,
                PersonalEmail = admin.PersonalEmail,
                PermissionsLevel = admin.PermissionsLevel
            }, commandType: CommandType.StoredProcedure);

            return admin;
        }

        public async void DropAdmin(AdminsModel admin)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            await dbConnection.ExecuteAsync("spAdmin_DropAdmin", new { id = admin.Id }, commandType: CommandType.StoredProcedure);
        }

        public async Task<InternsModel> GetArchivedInternById(int id)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            InternsModel archivedIntern = await dbConnection.QuerySingleOrDefaultAsync<InternsModel>("spAdmin_GetArchivedInternById", new { id = id }, commandType: CommandType.StoredProcedure);

            return archivedIntern;
        }

        public async Task<Task> ResurrectArchivedIntern(int id)
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            await dbConnection.QuerySingleOrDefaultAsync<int>("spAdmin_ResurrectArchivedIntern", new { id = id }, commandType: CommandType.StoredProcedure);
            return Task.CompletedTask;
        }

        public async Task<List<InternsModel>> GetAllArchivedInterns()
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            List<InternsModel> archivedInterns = (await dbConnection.QueryAsync<InternsModel>("spAdmin_GetAllArchivedInterns", new { }, commandType: CommandType.StoredProcedure)).ToList();

            return archivedInterns;
        }

        public async Task<List<AdminsModel>> GetAllAdmins()
        {
            string connString = _config.GetConnectionString("InternshipDb");
            using IDbConnection dbConnection = new SqlConnection(connString);

            List<AdminsModel> admins = (await dbConnection.QueryAsync<AdminsModel>("spAdmin_GetAllAdmins", new { }, commandType: CommandType.StoredProcedure)).ToList();

            return admins;
        }

    }
}
