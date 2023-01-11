namespace InternRegistrationForm.Classes
{
    public static class NameOfView
    {
        public static string ViewName(string viewName)
        { 
            return viewName.Substring(0 , viewName.LastIndexOf(".cshtml"));
        }

    }
}
