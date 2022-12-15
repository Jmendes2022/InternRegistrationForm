namespace InternRegistrationForm.Models
{
    public class AdminIndexViewModel
    {
        public AdminsModel adminsModel { get; set; }

        public List<InternsModel>? interns { get; set; }

        public InternsModel intern { get; set; }

        public List<AdminsModel> PendingAdminsList { get; set; } = null; 
    }
}
