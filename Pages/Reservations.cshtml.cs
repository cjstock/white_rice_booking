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
    public class ReservationsModel : PageModel
    {
        private readonly ILogger<ReservationsModel> _logger;

        public ReservationsModel(ILogger<ReservationsModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost() 
        {
            return RedirectToPage("./My_Trips");
        }
// NOTE TO SELF: (DELETE LATER)
        /* AFTER reservation & payment has been made, REDIRECT user to HOME PAGE (OR MY TRIPS PAGE) */
    }
}
