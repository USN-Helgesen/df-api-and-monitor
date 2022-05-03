using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DimensionFourMonitor.Models
{
    public class Point
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<string> types { get; set; }
        public Point(JToken json)
        {
            id = (string)json["id"];
            name = (string)json["name"];
            types = new List<string>();
            var metadata = json["metadata"];
            if (metadata != null && metadata["types"] != null)
            {
                foreach(string signalType in metadata["types"])
                {
                    types.Add(signalType);
                }
            }
        }
    }
}
