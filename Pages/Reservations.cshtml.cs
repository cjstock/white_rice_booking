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

        [BindProperty(SupportsGet = true)]
        public int depart_ID{ get; set; }

        [BindProperty(SupportsGet = true)]
        public int return_ID{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string first_name{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string last_name{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string street_address{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int apt_num{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string city{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string state{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int zip_code{ get; set; }


        public ReservationsModel(ILogger<ReservationsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost() 
        {
            //return RedirectToPage("/My_Trips");
            return Page();
        }
        // NOTE TO SELF: (DELETE LATER)
        /* AFTER reservation & payment has been made, REDIRECT user to HOME PAGE (OR MY TRIPS PAGE) */
    }
}