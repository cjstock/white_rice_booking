using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using white_rice_booking.Models;
using white_rice_booking.Services;


namespace white_rice_booking.Pages
{
    public class FlightStatusModel : PageModel
    {
        private readonly ILogger<FlightStatusModel> _logger;

        public FlightStatusModel(ILogger<FlightStatusModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost() 
        {
            return RedirectToPage("/Index");
        }

    }
}
