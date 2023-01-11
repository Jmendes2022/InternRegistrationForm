namespace InternRegistrationForm.Classes
{
    public class PasswordHasher : PWH
    {
        private readonly string _user;
        private readonly string _pass;

        public PasswordHasher(string user, string pass) : base(user, pass)
        {
            _user = user;
            _pass = pass;
        }

    }
}
