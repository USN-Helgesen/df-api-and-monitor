using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DimensionFourMonitor.Models
{
    public class Signal
    {
        public string id { get; set; }
        public string type { get; set; }
        public string unit { get; set; }
        public float value { get; set; }
        public DateTime timestamp { get; set; }
        public Signal(JToken json)
        {

            id = (string)json["id"];
            unit = (string)json["unit"];
            type = (string)json["type"];
            timestamp = (DateTime)json["timestamp"];
            value = (float)json["data"]["rawValue"];
        }

    }
}
