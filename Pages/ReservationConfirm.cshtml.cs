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
    public class ReservationConfirmModel : PageModel
    {
        private readonly ILogger<ReservationConfirmModel> _logger;
        
        private ReservationService _reservationService;
        
        public ReservationConfirmModel(ILogger<ReservationConfirmModel> logger, ReservationService reservationService)
        {
            _logger = logger;
            _reservationService = reservationService;
        }

        [BindProperty(SupportsGet = true)]
        public int depart_ID{ get; set; }

        [BindProperty(SupportsGet = true)]
        public int return_ID{ get; set; }

        [BindProperty(SupportsGet = true)]
        public string FirstName{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string LastName{ get; set; }

        [BindProperty(SupportsGet = true)]
        public long CardNumber{ get; set; }

        [BindProperty(SupportsGet = true)]
        public string ExpirationDate{ get; set; }

        [BindProperty(SupportsGet = true)]
        public int CVV{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string StreetName{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string City{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string State{ get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int ZipCode{ get; set; }

        [BindProperty(SupportsGet = true)]
        public int route_id{ get; set; }

        [BindProperty(SupportsGet = true)]
        public int depart_flight_id{ get; set; }

        [BindProperty(SupportsGet = true)]
        public int arrival_id{ get; set; }

        [BindProperty(SupportsGet = true)]
        public string depart_airport_name{ get; set; }

        [BindProperty(SupportsGet = true)]
        public string depart_city_name{ get; set; }

        [BindProperty(SupportsGet = true)]
        public string arrival_airport_name{ get; set; }

        [BindProperty(SupportsGet = true)]
        public string arrival_city_name{ get; set; }

        public void OnGet()
        { 
            //todo: seats, date, time, depart airport, depart city, arrival airport, arrival city, billing

            //calling flight information
            /*int flightID = 539567;
            string temp = _reservationService.FindFlightInfo(flightID);
            string[] words = temp.Split(','); //string array output 

            //Parsing the output string array back into variables
            route_id = Int32.Parse(words[0]);
            depart_flight_id = Int32.Parse(words[1]);
            arrival_id = Int32.Parse(words[2]);
            depart_airport_name = words[3];
            depart_city_name = words[4];
            arrival_airport_name = words[5];
            arrival_city_name = words[6];*/
        }

        public IActionResult OnPost()
        {
            int userID = 159753;
            _reservationService.Create(FirstName, LastName, depart_ID, return_ID, userID);
            //calling flight information
            int flightID = 539567;
            string temp = _reservationService.FindFlightInfo(flightID);
            string[] words = temp.Split(','); //string array output 

            //Parsing the output string array back into variables
            route_id = Int32.Parse(words[0]);
            depart_flight_id = Int32.Parse(words[1]);
            arrival_id = Int32.Parse(words[2]);
            depart_airport_name = words[3];
            depart_city_name = words[4];
            arrival_airport_name = words[5];
            arrival_city_name = words[6];
            return Page();
        }
    }
}