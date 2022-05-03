using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DimensionFourMonitor.Consumers;
using DimensionFourMonitor.Models;

namespace DimensionFourMonitor.Pages
{
    public class SignalDisplayModel : PageModel
    {
        private readonly DFourConsumer _consumer;
        public Space space;
        public Point point;
        public List<Point> points = new List<Point>();
        public List<Signal> signals = new List<Signal>();
        public SignalDisplayModel(DFourConsumer consumer)
        {
            _consumer = consumer;
        }
        public void OnGet()
        {
        }
        public JsonResult OnGetSpace(string id)
        {
            space = _consumer.GetSpace(id).Result;
            return new JsonResult(space);
        }
        public JsonResult OnGetPoint(string id)
        {
            point = _consumer.GetPoint(id).Result;
            return new JsonResult(point);
        }
        public JsonResult OnGetUpdateSignals(string id, string type, int paginate)
        {
            signals = _consumer.GetSignals(id, type, paginate).Result;
            return new JsonResult(signals);
        }
    }
}
