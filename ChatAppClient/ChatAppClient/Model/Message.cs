using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppClient.Model
{
    public class Message
    {
        public int ID { get; set; }

      
        public string Content { get; set; }

        public int AppUserID { get; set; }

        public AppUser AppUser { get; set; }
    }
}
