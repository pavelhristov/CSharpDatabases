using SuperheroesUniverse.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperheroesUniverse.Models
{
    public class Fraction
    {
        private ICollection<Superhero> superheroes;
        private ICollection<Planet> planets;

        public Fraction()
        {
            this.superheroes = new HashSet<Superhero>();
            this.planets = new HashSet<Planet>();
        }

        public int Id { get; set; }

        [MinLength(2), MaxLength(30), Index(IsUnique = true), Required]
        public string Name { get; set; }

        public virtual Alignment Alignment { get; set; }

        public virtual ICollection<Superhero> Superheroes
        {
            get
            {
                return this.superheroes;
            }
            set
            {
                this.superheroes = value;
            }
        }

        public virtual ICollection<Planet> Planets
        {
            get
            {
                return this.planets;
            }
            set
            {
                this.planets = value;
            }
        }
    }
}