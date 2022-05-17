using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DimensionFourMonitor.Consumers;

namespace DimensionFourMonitor.Models
{
    public class Space
    {
        private readonly DFourConsumer _consumer;
        public string id { get; set; }
        public string name { get; set; }
        public List<Point> points { get; set; }
        public List<Space> subspaces { get; set; }
        public Space(JToken json, DFourConsumer consumer)
        {
            _consumer = consumer;
            id = (string)json["id"];
            name = (string)json["name"];
            subspaces = new List<Space>();
            if (json["children"].HasValues) {
                foreach (var jSpace in json["children"]["edges"])
                {
                    if (jSpace != null)
                    {
                        var firstNode = jSpace.First;
                        if (firstNode != null)
                        {
                            var secondNode = firstNode.First;
                            if (secondNode != null)
                            {
                                Space space = _consumer.GetSpace((string)secondNode["id"]).Result;
                                subspaces.Add(space);
                            }
                        }
                    }
                }
            }
            points = new List<Point>();
            foreach (var jPoint in json["points"]["edges"])
            {
                if (jPoint != null)
                {
                    var firstNode = jPoint.First;
                    if (firstNode != null)
                    {
                        var secondNode = firstNode.First;
                        if (secondNode != null)
                        {
                            Point point = new Point(secondNode);
                            points.Add(point);
                        }
                    }
                }
            }
        }
    }
}
