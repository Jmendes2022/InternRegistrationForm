namespace InternRegistrationForm.Classes
{
    public static class NameOfController
    {
        public static string ControllerName(string controllerName)
        {
            return controllerName.Substring(0, controllerName.LastIndexOf("Controller"));
        }


    }
}
