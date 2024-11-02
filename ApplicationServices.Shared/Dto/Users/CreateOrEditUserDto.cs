using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Users
{
    public class CreateOrEditUserDto
    {
        [Required(ErrorMessage = "The ID is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "The email format is not valid")]
        [StringLength(100)]
        public string Email { get; set; }
        //[Required(ErrorMessage = "The User Type is required")]
        public int UserType { get; set; }
        [Required(ErrorMessage = "The Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The Middle Name is required")]
        public string MiddleName { get; set; }
        public int UpdateUserId { get; set; }
        public bool StatusId { get; set; }


        [Required(ErrorMessage = "The User Rol is required")]
        public int RolId { get; set; }
    }
}
