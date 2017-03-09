using SocialNetwork.Models;
using System.Data.Entity;

namespace SocialNetwork.Data
{
    public class SocialNetworkDbContext : DbContext
    {
        public SocialNetworkDbContext()
            : base("SocialNetwork")
        {
        }

        public virtual IDbSet<User> Users { get; set; }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<Image> Images { get; set; }

        public virtual IDbSet<Message> Messages { get; set; }

        public virtual IDbSet<Friendship> Friendships { get; set; }
    }
}
