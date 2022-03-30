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
        public Point(JObject json)
        {
            id = (string)json["id"];
            name = (string)json["name"];
        }
    }
}
