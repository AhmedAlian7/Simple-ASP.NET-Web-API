using Back_End.Models;

namespace Back_End.Authurization
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple =false)]
    public class CheckPermisionAttribute : Attribute
    {
        public CheckPermisionAttribute(Permissions permissions)
        {
            Permissions = permissions;
        }

        public Permissions Permissions { get; }
    }
}
