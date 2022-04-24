using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppClient.Model
{
    public class AppUser
    {
        public AppUser()
        {
            Messages = new HashSet<Message>();
        }
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
