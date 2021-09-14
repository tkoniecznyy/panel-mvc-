using System;
using System.ComponentModel.DataAnnotations;
using VIP_Panel.Data;

namespace VIP_Panel.Models
{
    public class PostModel
    {
       // private ApplicationDbContext _context;
        public int ID { get; set; }
        [Required]
        public string Content { get; set; }
        [DataType(DataType.DateTime)]
        public string Date { get; set; }
        public int UserID { get; set; }    //FK in database contains id from VIPUser table

        public PostModel(int id, string content, string dateTime, int userID)
        {
            ID = id;
            Content = content;
            Date = dateTime;
            UserID = userID;
        }

        public PostModel()
        {

        }
    }
}
