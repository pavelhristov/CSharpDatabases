using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleClient
{
    class StartUp
    {
        public class Link
        {
            [JsonProperty("@href")]
            public string href { get; set; }
        }
        public class Video
        {
            public string id { get; set; }
            public string title { get; set; }
            public Link link { get; set; }
        }

        static void Main(string[] args)
        {
            // last minute job, magic flies everywhere, please ignore my anti-HQC
            // check Data folder in explorer for some of the files
            string downloadUrl = @"https://www.youtube.com/feeds/videos.xml?channel_id=UCLC-vbm7OWvpbqzXaoAMGGw";
            string feedUrl = @"../../Data/feed.xml";
            string jsonUrl = @"../../Data/feed.json";

            WebClient webClient = new WebClient();
            webClient.DownloadFile(downloadUrl, feedUrl);

            XmlDocument doc = new XmlDocument();
            doc.Load(feedUrl);
            string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(jsonUrl, jsonText);

            var jsonObj = JObject.Parse(jsonText);

            var videoTitles = from entry in jsonObj["feed"]["entry"]
                              select entry["title"];
            Console.WriteLine(string.Join("\r\n", videoTitles));

            var videos = from feed in jsonObj["feed"]["entry"]
                         select feed;
            var videoList = new List<Video>();
            foreach (var item in videos)
            {
                videoList.Add(JsonConvert.DeserializeObject<Video>(item.ToString()));
            }

            const string indexHtml = @"../../Data/index.html";
            const string templateHtml = @"../../Data/template.html";

            var content = File.ReadAllText(templateHtml);

            StringBuilder builder = new StringBuilder("<ul>");
            foreach (var item in videoList)
            {
                builder.AppendLine($"<li><a href=\"{item.link.href}\">{item.title}</a></li>");
            }

            builder.AppendLine("</ul>");

            content = content.Replace("{CONTAINER}", builder.ToString());

            File.WriteAllText(indexHtml, content);
        }
    }
}
