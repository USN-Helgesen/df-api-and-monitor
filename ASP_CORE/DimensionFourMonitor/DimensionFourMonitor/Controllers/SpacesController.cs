using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DimensionFourMonitor.Consumers;
using System.Web;

namespace DimensionFourMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpacesController : Controller
    {
        private readonly DFourConsumer _consumer;
        public SpacesController(DFourConsumer consumer)
        {
            _consumer = consumer;
        }
        public async Task<IActionResult> ViewSpacesList()
        {
            var spaces = await _consumer.GetAllSpaces();
            return View(spaces);
        }
    }
}
