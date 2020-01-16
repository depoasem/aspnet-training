using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApps.Models;

namespace DemoApps.Repository
{
    public interface IAccountRepository
    {
        // tblUser AddUsers(tblUser item);
        UserResponse AddUser(UserResponse item);
        bool DeleteUser(int id);
        IEnumerable<UserResponse> GetAllUser();
        UserResponse GetUser(int id);
        UserResponse CheckUser(loginResponse item);
        bool UpdateUser(UserResponse item);

    }
}
