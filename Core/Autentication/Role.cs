using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Autentication
{
    public class Role : IdentityRole<int>
    {
        public Role() { }
        public Role(String roleName):base(roleName) { }
    }
}
