using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperheroesUniverse.Models
{
    public class Planet
    {
        private ICollection<Country> countries;

        public Planet()
        {
            this.countries = new HashSet<Country>();
        }
        public int Id { get; set; }

        [MinLength(2), MaxLength(30), Index(IsUnique = true), Required]
        public string Name { get; set; }

        public virtual ICollection<Country> Countries
        {
            get
            {
                return this.countries;
            }
            set
            {
                this.countries = value;
            }
        }
    }
}