using SuperheroesUniverse.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperheroesUniverse.Models
{
    public class Superhero
    {
        private ICollection<Fraction> fractions;
        private ICollection<Power> powers;

        public Superhero()
        {
            this.fractions = new HashSet<Fraction>();
            this.powers = new HashSet<Power>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(3), MaxLength(60), Required]
        public string Name { get; set; }

        [MinLength(3), MaxLength(20), Index(IsUnique = true), Required]
        public string SecretIdentity { get; set; }

        [Required]
        public virtual Alignment Alignment { get; set; }

        [MinLength(1), Required]
        public string Story { get; set; }

        [Required]
        public virtual City City { get; set; }

        [Required]
        public virtual ICollection<Fraction> Fractions
        {
            get
            {
                return this.fractions;
            }
            set
            {
                this.fractions = value;
            }
        }

        [Required]
        public virtual ICollection<Power> Powers
        {
            get
            {
                return this.powers;
            }
            set
            {
                this.powers = value;
            }
        }
    }
}
