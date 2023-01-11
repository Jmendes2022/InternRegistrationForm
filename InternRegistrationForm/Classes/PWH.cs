using Microsoft.AspNetCore.Identity;

namespace InternRegistrationForm.Classes
{
    public abstract class PWH
    {
        public string User { get; set; } = default!;
        public string Pass { get; set; } = default!;

        
        public void SetHashedPass(string user, string pass)
        { 
            PasswordHasher<dynamic> passwordHasher = new PasswordHasher<dynamic>();
            var hashedPass = passwordHasher.HashPassword(user, pass);

            User = user;
            Pass = hashedPass;
        }
        
        public PWH(string user, string pass)
        {
            SetHashedPass(user, pass);
        }
    }
}
