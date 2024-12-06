using Microsoft.AspNetCore.Identity;

namespace Core.Autentication
{
    public class Role : IdentityRole<int>
    {
        public Role() { }
        public Role(String roleName):base(roleName) { }
    }
}
