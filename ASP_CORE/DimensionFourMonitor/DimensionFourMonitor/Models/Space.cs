using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DimensionFourMonitor.Models
{
    public class Space
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Point> points { get; set; }
        public Space(JToken json)
        {
            id = (string)json["id"];
            name = (string)json["name"];
            points = new List<Point>();
            foreach (JObject jPoint in json["points"])
            {
                Point point = new Point(jPoint);
                points.Add(point);
            }
        }
    }
}
