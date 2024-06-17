using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using SASC_Final.Models.Common.AuthModels;

namespace SASC_Final.Services
{
    public interface IAuth
    {
        //Task<string> Login(string username, string password);
        Task<string> Login(LoginModel model);
        Task<string> Register(RegistrationModel model);
        //string Register(UserRegisterModel model)
        Task Logout();
    }
}
