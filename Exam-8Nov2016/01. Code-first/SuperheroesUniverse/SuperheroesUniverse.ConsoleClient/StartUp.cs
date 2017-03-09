using Ninject;
using SuperheroesUniverse.ConsoleClient.Modules;
using SuperheroesUniverse.Data;
using SuperheroesUniverse.JsonProcessor;
using SuperheroesUniverse.Models;
using SuperheroesUniverse.Models.Enums;
using System;
using System.Linq;
using XmlReporter;

namespace SuperheroesUniverse.ConsoleClient
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new EntityFrameworkModule());

            var dataProvider = kernel.Get<DataProvider>();
            string filePath = "../../../../../02. Json-Data/sample-data.json";

            var jsonData = JsonParser.GetJsonData(filePath);

            using (var uow = dataProvider.UnitOfWork())
            {
                // please ignore this totally anti-HQC code :/
                foreach (var data in jsonData)
                {
                    if (dataProvider.Superheroes.GetAll(x => x.Name == data.name).FirstOrDefault() == null)
                    {
                        var superhero = new Superhero();
                        superhero.Name = data.name;
                        superhero.SecretIdentity = data.secretIdentity;
                        superhero.Story = data.story;
                        AlignmentType alignmentType;
                        if (Enum.TryParse<AlignmentType>("evil", out alignmentType))
                        {
                            superhero.Alignment = new Alignment() { AlignmentValue = alignmentType };
                        }

                        foreach (var item in data.powers)
                        {
                            var power = dataProvider.Powers.GetAll(x => x.Name == item).FirstOrDefault();
                            if (power == null)
                            {
                                power = new Power()
                                {
                                    Name = item
                                };
                            }
                            superhero.Powers.Add(power);
                        }

                        Planet planet = dataProvider.Planets.GetAll(x => x.Name == data.city.planet).FirstOrDefault();
                        Country country = dataProvider.Countries.GetAll(x => x.Name == data.city.country).FirstOrDefault();
                        City city = dataProvider.Cities.GetAll(x => x.Name == data.city.name).FirstOrDefault();
                        if (planet == null)
                        {
                            city = new City()
                            {
                                Name = data.city.name,
                                Country = new Country()
                                {
                                    Name = data.city.country,
                                    Planet = new Planet()
                                    {
                                        Name = data.city.planet
                                    }
                                }
                            };
                        }
                        else if (country == null)
                        {
                            city = new City()
                            {
                                Name = data.city.name,
                                Country = new Country()
                                {
                                    Name = data.city.country,
                                    Planet = planet
                                }
                            };
                        }
                        else if (city == null)
                        {
                            city = new City()
                            {
                                Name = data.city.name,
                                Country = country
                            };
                        }

                        superhero.City = city;

                        foreach (var item in data.fractions)
                        {
                            var fraction = dataProvider.Fractions.GetAll(x => x.Name == item).FirstOrDefault();
                            if (fraction == null)
                            {
                                fraction = new Fraction()
                                {
                                    Name = item
                                };
                            }
                            superhero.Fractions.Add(fraction);
                        }

                        dataProvider.Superheroes.Add(superhero);
                        uow.Commit();
                    }
                }
            }
        }
    }
}
