using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BBL.DTOs;

namespace WebAPI.BBL.Interfaces
{
    public interface IUserService
    {
        void SignUpUser(UserDTO userDto);
        bool SignInUser(UserDTO userDto);
        UserDTO GetUser(int? id);
        IEnumerable<UserDTO> GetUsers();
        bool DeleteUser(int? id);
        bool EditUser(UserDTO userDto);
        void Dispose();
    }
}
