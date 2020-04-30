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
    public class ChosenFlightModel : PageModel
    {
        private readonly ILogger<ChosenFlightModel> _logger;

        public ChosenFlightModel(ILogger<ChosenFlightModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost() 
        {
            return RedirectToPage("/Reservations");
        }
// NOTE TO SELF: (DELETE LATER)
        /* AFTER user selects a flight (which is listed with a price in $), REDIRECT user to RESERVATIONS PAGE */
    }
}
