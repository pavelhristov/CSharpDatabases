using SuperheroesUniverse.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SuperheroesUniverse.Models
{
    public class Relationship
    {
        public int Id { get; set; }

        public int SuperheroId1 { get; set; }
        public Superhero Superhero1 { get; set; }

        public int SuperheroId2 { get; set; }
        public Superhero Superhero2 { get; set; }

        [Required]
        public RelationshipType RelationshipType { get; set; }
    }
}
