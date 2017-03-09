using SuperheroesUniverse.Data.Repositories.Contracts;
using SuperheroesUniverse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperheroesUniverse.Data
{
    public class DataProvider
    {
        private IGenericRepository<Superhero> superheroes;

        private IGenericRepository<City> cities;

        private IGenericRepository<Country> countries;

        private IGenericRepository<Planet> planets;

        private IGenericRepository<Fraction> fractions;

        private IGenericRepository<Relationship> relationships;

        private IGenericRepository<Power> powers;

        private IGenericRepository<Alignment> alignments;

        private Func<IUnitOfWork> unitOfWork;

        public DataProvider(Func<IUnitOfWork> unitOfWork,
                            IGenericRepository<Superhero> superheroes,
                            IGenericRepository<City> cities,
                            IGenericRepository<Country> countries,
                            IGenericRepository<Planet> planets,
                            IGenericRepository<Fraction> fractions,
                            IGenericRepository<Relationship> relationships,
                            IGenericRepository<Power> powers,
                            IGenericRepository<Alignment> alignments)
        {
            this.UnitOfWork = unitOfWork;
            this.Superheroes = superheroes;
            this.Cities = cities;
            this.Countries = countries;
            this.Planets = planets;
            this.Fractions = fractions;
            this.Relationships = relationships;
            this.Powers = powers;
            this.Alignments = alignments;
        }

        public IGenericRepository<Superhero> Superheroes
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

        public IGenericRepository<City> Cities
        {
            get
            {
                return cities;
            }

            set
            {
                cities = value;
            }
        }

        public IGenericRepository<Country> Countries
        {
            get
            {
                return countries;
            }

            set
            {
                countries = value;
            }
        }

        public IGenericRepository<Planet> Planets
        {
            get
            {
                return planets;
            }

            set
            {
                planets = value;
            }
        }

        public IGenericRepository<Fraction> Fractions
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

        public IGenericRepository<Relationship> Relationships
        {
            get
            {
                return relationships;
            }

            set
            {
                relationships = value;
            }
        }

        public IGenericRepository<Power> Powers
        {
            get
            {
                return powers;
            }

            set
            {
                powers = value;
            }
        }

        public Func<IUnitOfWork> UnitOfWork
        {
            get
            {
                return unitOfWork;
            }

            set
            {
                unitOfWork = value;
            }
        }

        public IGenericRepository<Alignment> Alignments
        {
            get
            {
                return alignments;
            }

            set
            {
                alignments = value;
            }
        }
    }
}
