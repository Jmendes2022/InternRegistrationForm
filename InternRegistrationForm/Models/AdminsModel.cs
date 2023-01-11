using System.ComponentModel.DataAnnotations;

namespace InternRegistrationForm.Models
{
    public class AdminsModel
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string? FirstName { get; set; } = default!;
        
        [Required]
        public string? LastName { get; set; }  = default!;
        
        [Required]
        public string? Username { get; set; }  = default!;
        
        [Required]
        public string? Password { get; set; }  = default!;

        public string? InternshipEmail { get; set; }

        public string? SchoolEmail { get; set; }

        public string? PersonalEmail { get; set; }

        [Required]
        public string? Department { get; set; } = default!;



        //0 => no permission.
        //1 => Everything but editing admin accounts, dropping, archiving, and resurrecting interns.
        //2 => Complete control to do everything.
        public int PermissionsLevel { get; set; } = 0;



    }
}
