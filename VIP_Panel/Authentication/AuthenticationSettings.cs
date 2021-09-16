using System;
namespace VIP_Panel.Authentication
{
    public class AuthenticationSettings
    {

        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; }


        public AuthenticationSettings()
        {
           
        }



    }
}
