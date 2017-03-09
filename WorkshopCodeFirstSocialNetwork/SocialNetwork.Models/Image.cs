using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [MaxLength(4), Required]
        public string FileExtension { get; set; }

        public User User { get; set; }
    }
}