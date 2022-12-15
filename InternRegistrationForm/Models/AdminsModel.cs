namespace InternRegistrationForm.Models
{
    public class AdminsModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Department { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool HasPermissions { get; set; } = false;

    }
}
