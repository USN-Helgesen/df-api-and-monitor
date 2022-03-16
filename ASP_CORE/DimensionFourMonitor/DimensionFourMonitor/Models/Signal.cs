using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DimensionFourMonitor.Models
{
    public class Signal
    {
        public string SignalId { get; set; }
        public string SignalType { get; set; }
        public string SignalUnit { get; set; }
        public float SignalValue { get; set; }
        public string SignalTimestamp { get; set; }

    }
}
