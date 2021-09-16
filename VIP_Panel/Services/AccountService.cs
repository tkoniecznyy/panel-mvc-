using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using VIP_Panel.Authentication;
using VIP_Panel.Data;
using VIP_Panel.Exceptions;
using VIP_Panel.Models;

namespace VIP_Panel.Services
{
    public class AccountService : IAccountService
    {

        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<VipUserModel> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;


        public AccountService(ApplicationDbContext context, IPasswordHasher<VipUserModel> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public void RegisterUser(VipUserModel dto)
        {
            var newUser = new VipUserModel()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = dto.Password
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.Password = hashedPassword;
            _context.vipUsers.Add(newUser);
            _context.SaveChanges();
        }



        public string GenerateJwt(LoginDto dto)
        {
            var user = _context.vipUsers.FirstOrDefault(u => u.Email == dto.Email);

            if (user is null)
            {
                throw new BadRequestException("Invalid username or password!");
            }

          //     var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
          //  if (result == PasswordVerificationResult.Failed)
          //  {
          //      throw new BadRequestException("Invalid username or password!");
          //  }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, $"{user.Email}")
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expiration,
                signingCredentials: credentials
                );


            var tokenHandler = new JwtSecurityTokenHandler();


            return tokenHandler.WriteToken(token);

        }
    }
}
