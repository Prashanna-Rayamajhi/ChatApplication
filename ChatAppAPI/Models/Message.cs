using System.ComponentModel.DataAnnotations;

namespace ChatAppAPI.Models
{
    public class Message
    {
        public int ID { get; set; }

        [StringLength(500, ErrorMessage = "Text message can be upto 500 characters long")]
        public string Content { get; set; }

        public int AppUserID { get; set; }

        public  AppUser AppUser { get; set; }
    }
}