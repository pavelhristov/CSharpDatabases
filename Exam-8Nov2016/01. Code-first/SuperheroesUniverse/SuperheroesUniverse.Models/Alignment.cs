using SuperheroesUniverse.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperheroesUniverse.Models
{
    public class Alignment
    {
        private ICollection<Superhero> superheroes;
        private ICollection<Fraction> fractions;

        public Alignment()
        {
            this.superheroes = new HashSet<Superhero>();
            this.fractions = new HashSet<Fraction>();
        }

        public int Id { get; set; }

        public AlignmentType AlignmentValue { get; set; }

        public ICollection<Superhero> Superheroes
        {
            get
            {
                return superheroes;
            }

            set
            {
                superheroes = value;
            }
        }

        public ICollection<Fraction> Fractions
        {
            get
            {
                return fractions;
            }

            set
            {
                fractions = value;
            }
        }
    }
}
