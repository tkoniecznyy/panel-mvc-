using System;
using VIP_Panel.Models;

namespace VIP_Panel.Services
{
    public interface IAccountService
    {
        string GenerateJwt(LoginDto dto);
        void RegisterUser(VipUserModel dto);


    }
}
