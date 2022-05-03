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
    public class SpacesListModel : PageModel
    {
        private readonly DFourConsumer _consumer;
        public List<Space> spaces = new List<Space>();
        public SpacesListModel(DFourConsumer consumer)
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
    }
}
