namespace InternRegistrationForm.Models
{
    public class InternsModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Department { get; set; }
        public string? DegreePlan { get; set; }
        public string? Role { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Today;
        public string? DegreeConcentration { get; set; }
        public string? PersonalEmail { get; set; }
        public string?  SchoolEmail { get; set; }
        public string? InternshipEmail { get; set; }
        public string? Track { get; set; }
        public bool InviteToMeetings { get; set; } = false;
        public bool Email { get; set; } = false;
        public bool EntranceSurvey { get; set; } = false;
        public bool WelcomeDocument { get; set; } = false;
        public bool ThreeSignedDocuments { get; set; } = false;
        public bool ESETTraining { get; set; } = false;
        public bool Resume { get; set; } = false;
        public bool Orientation1Access { get; set; } = false;
        public bool Orientation2Access { get; set; } = false;
        public bool TCWAccess { get; set; } = false;
        public bool PlannerAccess { get; set; } = false;
        public bool TeamGroupChat { get; set; } = false;
        public bool OneDriveSetup { get; set; } = false;
        public bool AzureSetup { get; set; } = false;
        public bool ExitSurveySent { get; set; } = false;
        public bool Masterclass { get; set; } = false;
        public string? InternNotes { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdate { get; set; } = DateTime.Today;

    }
}
