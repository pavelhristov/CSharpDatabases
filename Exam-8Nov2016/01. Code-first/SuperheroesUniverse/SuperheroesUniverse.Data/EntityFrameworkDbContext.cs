using SuperheroesUniverse.Data.Contracts;
using SuperheroesUniverse.Data.Migrations;
using SuperheroesUniverse.Models;
using System.Data.Entity;

namespace SuperheroesUniverse.Data
{
    public class EntityFrameworkDbContext : DbContext, IDbContext
    {
        public EntityFrameworkDbContext()
            : base("SuperheroesUniverse")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EntityFrameworkDbContext, Configuration>());
        }

        public virtual IDbSet<Superhero> Superheroes { get; set; }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<Country> Countries { get; set; }

        public virtual IDbSet<Planet> Planets { get; set; }

        public virtual IDbSet<Fraction> Fractions { get; set; }

        public virtual IDbSet<Relationship> Relationships { get; set; }

        public virtual IDbSet<Power> Powers { get; set; }

        public virtual IDbSet<Alignment> Alignments { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public void CreateDatabaseIfNotExists()
        {
            base.Database.CreateIfNotExists();
        }
    }
}
