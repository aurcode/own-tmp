using Dto.Catalog;
using Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Catalogs.Other
{
    public interface IOtherService
    {
        //Task<List<UsersResults>> GetUsers();
        //Task<UsersResults> GetUserById(int id);
        Task<List<UserTypeDto>> getUserTypes();
    }
}
