using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Interfaces
{
    public interface IUserService : IDisposable
    {
        UserModel GetUser(int? id);
        IEnumerable<UserModel> GetUsers();
        bool DeleteUser(int? id);
        bool EditUser(UserModel user, string password);
    }
}
