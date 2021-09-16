﻿using System;
namespace VIP_Panel.Models
{
    public class LoginDto
    {

        public string Email { get; set; }

        public string Password { get; set; }


        public LoginDto()
        {
        }

        public LoginDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
