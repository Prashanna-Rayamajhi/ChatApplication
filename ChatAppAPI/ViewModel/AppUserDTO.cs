using ChatAppAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppAPI.ViewModel
{
    public class AppUserDTO
    {
        public AppUserDTO()
        {
            Messages = new HashSet<Message>();
        }
        public int ID { get; set; }

        [Required(ErrorMessage = "User name cannot be empty")]
        [StringLength(25, ErrorMessage = "Username cannot be longer than 25 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email cannot be empty")]
        [StringLength(50, ErrorMessage = "Email cannot be longer than 50 character")]
        public string Email { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
