using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using SASC_Final.Models.Common.AuthModels;

namespace SASC_Final.Services
{
    public interface IAuth
    {
        Task<string> Login(LoginModel model);
        Task<string> Register(RegistrationModel model);
        Task Logout();
    }
}
