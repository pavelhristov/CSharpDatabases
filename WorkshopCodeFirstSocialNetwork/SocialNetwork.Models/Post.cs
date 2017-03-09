using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{
    public class Post
    {
        private ICollection<User> taggedUsers;

        public Post()
        {
            this.taggedUsers = new HashSet<User>();
        }
        public int Id { get; set; }

        [MinLength(10), Required]
        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public virtual ICollection<User> TaggedUsers { get; set; }

        public virtual User User { get; set; }
    }
}