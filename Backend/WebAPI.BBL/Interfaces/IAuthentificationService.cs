using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAuthentificationService
    {
        void SignUp(AuthentificationModel model);
        bool SignIn(AuthentificationModel model);
    }
}
