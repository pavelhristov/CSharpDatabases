using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    public class User
    {
        private ICollection<Post> posts;
        private ICollection<Message> messages;
        private ICollection<Image> images;

        public User()
        {
            this.posts = new HashSet<Post>();
            this.messages = new HashSet<Message>();
            this.images = new HashSet<Image>();
        }

        public int Id { get; set; }

        [MaxLength(20), MinLength(4), Required]
        public string Username { get; set; }

        [MaxLength(50), MinLength(2)]
        public string FirstName { get; set; }

        [MaxLength(50), MinLength(2)]
        public string LastName { get; set; }

        public DateTime RegisteredOn { get; set; }

        public virtual ICollection<Post> Posts
        {
            get
            {
                return this.posts;
            }
            set
            {
                this.posts = value;
            }
        }

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

        public virtual ICollection<Image> Images
        {
            get
            {
                return this.images;
            }
            set
            {
                this.images = value;
            }
        }
    }
}
