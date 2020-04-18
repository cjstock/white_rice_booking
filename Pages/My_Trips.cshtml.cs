using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace white_rice_booking.Pages
{
    public class MyTripsModel : PageModel
    {
        private readonly ILogger<MyTripsModel> _logger;

        public MyTripsModel(ILogger<MyTripsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
