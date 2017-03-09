using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    public class Friendship
    {
        private ICollection<Message> messages;

        public int Id { get; set; }

        public Friendship()
        {
            this.messages = new HashSet<Message>();
        }

        public int UserId1 { get; set; }
        public User User1 { get; set; }

        public int UserId2 { get; set; }
        public User User2 { get; set; }

        public DateTime FriendsSince { get; set; }

        public virtual ICollection<Message> Messages
        {
            get
            {
                return this.messages;
            }
            set
            {
                this.messages = value;
            }
        }

        [Index]
        public bool Approved { get; set; }
    }
}
