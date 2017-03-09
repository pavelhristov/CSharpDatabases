using SuperheroesUniverse.Data;
using SuperheroesUniverse.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XmlReporter
{
    public class SuperheroesUniverseExporter : ISuperheroesUniverseExporter
    {
        private DataProvider dp;
        public SuperheroesUniverseExporter(DataProvider dp)
        {
            this.dp = dp;
        }
        public string ExportAllSuperheroes()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(Superhero));
            var superhero  = dp.Superheroes.GetAll().FirstOrDefault(x => x.Id == 1);
            string xml = string.Empty;

            StringWriter sww = new StringWriter();
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, superhero);
                xml = sww.ToString(); // Your XML
            }

            return xml;
        }

        public string ExportFractionDetails(object fractionId)
        {
            throw new NotImplementedException();
        }

        public string ExportFractions()
        {
            throw new NotImplementedException();
        }

        public string ExportSuperheroDetails(object superheroId)
        {
            throw new NotImplementedException();
        }

        public string ExportSuperheroesByCity(string cityName)
        {
            throw new NotImplementedException();
        }

        public string ExportSupperheroesWithPower(string power)
        {
            throw new NotImplementedException();
        }
    }
}
