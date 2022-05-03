using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DimensionFourMonitor.Consumers;
using DimensionFourMonitor.Models;
using Newtonsoft.Json.Linq;

namespace DimensionFourMonitor.Pages
{
    public class MasterPanelModel : PageModel
    {
        private readonly DFourConsumer _consumer;
        public List<Space> spaces = new List<Space>();
        public List<Signal> signals = new List<Signal>();
        public MasterPanelModel (DFourConsumer consumer)
        {
            _consumer = consumer;
        }
        public void OnGet()
        {
            PrepSpaces();
        }
        public List<Space> PrepSpaces()
        {
            spaces = _consumer.GetAllSpaces().Result;
            return spaces;
        }
        public JsonResult OnGetUpdateSignals(string id, string type, int paginate)
        {
            signals = _consumer.GetSignals(id, type, paginate).Result;
            return new JsonResult(signals);
        }
    }
}
