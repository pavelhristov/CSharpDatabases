using SuperheroesUniverse.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperheroesUniverse.Data.Contracts
{
    public interface IDbContext
    {
        IDbSet<Superhero> Superheroes { get; set; }

        IDbSet<City> Cities { get; set; }

        IDbSet<Country> Countries { get; set; }

        IDbSet<Planet> Planets { get; set; }

        IDbSet<Fraction> Fractions { get; set; }

        IDbSet<Relationship> Relationships { get; set; }

        IDbSet<Power> Powers { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();

        // 100% sure this should not be here, will refactor if enough time.
        void CreateDatabaseIfNotExists();
    }
}
