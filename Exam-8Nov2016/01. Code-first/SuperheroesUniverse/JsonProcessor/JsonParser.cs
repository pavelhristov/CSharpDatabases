using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace SuperheroesUniverse.JsonProcessor
{
    public static class JsonParser
    {
        public static ICollection<data> GetJsonData(string filePath)
        {
            var jsonContent = File.ReadAllText(filePath);

            var dataList = JsonConvert.DeserializeObject<dataList>(jsonContent);

            return dataList.data;
        }

        public class data
        {
            public data()
            {
                this.powers = new HashSet<string>();
                this.fractions = new HashSet<string>();
            }

            public string name { get; set; }
            public string secretIdentity { get; set; }
            public JsonCity city { get; set; }
            public string alignment { get; set; }
            public string story { get; set; }
            public ICollection<string> powers { get; set; }
            public ICollection<string> fractions { get; set; }
        }

        public class dataList
        {
            public ICollection<data> data { get; set; }
        }

        public class JsonCity
        {
            public string name { get; set; }
            public string country { get; set; }
            public string planet { get; set; }
        }


    }
}
