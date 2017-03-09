using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    public class Message
    {
        public int Id { get; set; }

        public Friendship Friendship { get; set; }

        [Column("Author")]
        public User User { get; set; }

        [Required]
        public string Content { get; set; }

        [Index]
        public DateTime SentOn{ get; set; }

        public DateTime? SeenOn { get; set; }
    }
}