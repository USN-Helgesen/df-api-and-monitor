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
    public class AdminPanelModel : PageModel
    {
        private readonly DFourConsumer _consumer;
        public AdminPanelModel(DFourConsumer consumer)
        {
            _consumer = consumer;
        }
        public void OnGet()
        {
            _consumer.GetPoint("61ea7ac6406bf2319d84bc01");
        }
    }
}
