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
    public class MyTripsModel : PageModel
    {
        private readonly ILogger<MyTripsModel> _logger;
        private ReservationService _reservationService;

        public MyTripsModel(ILogger<MyTripsModel> logger, ReservationService reservationService)
        {
            _logger = logger;
            _reservationService = reservationService;
        }


        [BindProperty(SupportsGet = true)]        
        public int reservation_ID { get; set; }

        [BindProperty(SupportsGet = true)]
        public int flight_ID { get; set; }

        [BindProperty(SupportsGet = true)]
        public string DepartureAirport { get; set; }

        [BindProperty(SupportsGet = true)]
        public string DepartureCity { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ArrivalAirport { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ArrivalCity { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Date { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Time { get; set; }


        public void OnGet()
        {
            // Reservation ID

            // Outbound IDs and Inbound IDs used to display date,time, airport names

            //_reservationService.
        } 

        public IActionResult OnPost()
        {
            return Page();
        
        }
    }
}
