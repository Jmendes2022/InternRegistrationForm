namespace InternRegistrationForm.Models
{
    public class LogInViewModel
    {
        public AdminsModel adminsModel { get; set; }
        public bool LoggedAttemptFailed { get; set; } = false;

        public bool AlreadyExists { get; set; } = false;

        public List<AdminsModel> AllAdmins { get; set; }

    }
}
