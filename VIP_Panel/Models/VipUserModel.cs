using System;
using System.ComponentModel.DataAnnotations;

namespace VIP_Panel.Models
{
    public class VipUserModel
    {

        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public VipUserModel(int iD, string firstName, string lastName, string email, string password)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public VipUserModel()
        {
        }
    }
}
