using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DimensionFourMonitor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public void OnGetCreateCredentials(string tenantId, string tenantKey)
        {
            Console.WriteLine(tenantId);
            Console.WriteLine(tenantKey);
            HttpContext.Session.SetString("Tenant Id", tenantId);
            HttpContext.Session.SetString("Tenant Key", tenantKey);
        }
    }
}
