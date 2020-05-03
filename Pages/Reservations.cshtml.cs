/*
    Name: Filter Flights Service
    Date Last Updated: 5-1-2020
    Programmer Names: Timothy Bui, Justin Tran, Corey Stock
    Description: This class shall retrieve the outbound and inbound flight IDs from the 
                 Filter Flights back-end (FilterFlights.cshtml.cs), connect the front-end Reservations 
                 page with the Reservations Service, obtain user's personal billing information, 
                 display the information of the user's selected flights, and allows the user to
                 verify that all of the flight information is correct before finishing the
                 flight booking process.
    Important Methods/Structures/Etc: 
            - Functions - OnGet, OnPostConfirm
    Major Decisions: 
            - Used the HttpContext session for receiving data from a different PageModel since it 
              provides easy access to the stored values (from the other PageModel)
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using white_rice_booking.Models;
using white_rice_booking.Services;
using Microsoft.AspNetCore.Http;

namespace white_rice_booking.Pages
{
    public class ReservationsModel : PageModel
    {
        private readonly ILogger<ReservationsModel> _logger;
        private ReservationService _reservationService;

        public ReservationsModel(ILogger<ReservationsModel> logger, 
                                ReservationService reservationService)
        {
            _logger = logger;
            _reservationService = reservationService;
        }

        [BindProperty(SupportsGet = true)]
        public int outbound_ID { get; set; } // Outbound flight ID

        [BindProperty(SupportsGet = true)]
        public int inbound_ID { get; set; } // Inbound flight ID

        [BindProperty(SupportsGet = true)]
        public string FirstName{ get; set; } // First name of user/passenger
        
        [BindProperty(SupportsGet = true)]
        public string LastName{ get; set; } // Last name of user/passenger

        [BindProperty(SupportsGet = true)]
        public long CardNumber{ get; set; } // Credit card number for billing information

        [BindProperty(SupportsGet = true)]
        public string ExpirationDate{ get; set; } // Expiration date of credit card

        [BindProperty(SupportsGet = true)]
        public int CVV{ get; set; } // CVV of credit card
        
        [BindProperty(SupportsGet = true)]
        public string StreetName{ get; set; } // Street name of user/passenger's residence
        
        [BindProperty(SupportsGet = true)]
        public string City{ get; set; } // City of user/passenger's residence
        
        [BindProperty(SupportsGet = true)]
        public string State{ get; set; } // State of residence (for user/passenger)
        
        [BindProperty(SupportsGet = true)]
        public int ZipCode{ get; set; } // Zip code of user/passenger's residence

        [BindProperty(SupportsGet = true)]
        public int outbound_route_id{ get; set; } // Outbound route ID (for finding airports)

        [BindProperty(SupportsGet = true)]
        public int outbound_depart_flight_id{ get; set; } // Outbound departure flight ID 

        [BindProperty(SupportsGet = true)]
        public int outbound_arrival_id{ get; set; } // Outbound arrival airport ID

        [BindProperty(SupportsGet = true)]
        public string outbound_depart_airport_name{ get; set; } // Name of departure airport 
                                                                // (for outbound flight)
        [BindProperty(SupportsGet = true)]
        public string outbound_depart_city_name{ get; set; } // City name of departure location 
                                                             // (for outbound flight)

        [BindProperty(SupportsGet = true)]
        public string outbound_arrival_airport_name{ get; set; } // Name of arrival airport 
                                                                 // (for outbound flight)
        [BindProperty(SupportsGet = true)]
        public string outbound_arrival_city_name{ get; set; } // City name of arrival location
                                                              // (for outbound flight)
        [BindProperty(SupportsGet = true)]
        public int inbound_route_id{ get; set; } // Inbound route ID (for finding airports)

        [BindProperty(SupportsGet = true)]
        public int inbound_depart_flight_id{ get; set; } // Inbound departure flight ID

        [BindProperty(SupportsGet = true)]
        public int inbound_arrival_id{ get; set; } // Inbound arrival airport ID

        [BindProperty(SupportsGet = true)]
        public string inbound_depart_airport_name{ get; set; } // Name of departure airport 
                                                               // (for inbound flight)
        [BindProperty(SupportsGet = true)]
        public string inbound_depart_city_name{ get; set; } // City name of departure location 
                                                            // (for inbound flight)
        [BindProperty(SupportsGet = true)]
        public string inbound_arrival_airport_name{ get; set; } // Name of arrival airport 
                                                                 // (for inbound flight)
        [BindProperty(SupportsGet = true)]
        public string inbound_arrival_city_name{ get; set; } // City name of arrival location
                                                              // (for inbound flight)
        [BindProperty(SupportsGet = true)]
        public string TripType { get; set; } // Trip type (one-way or round trip)

        [BindProperty(SupportsGet = true)]
        public string outbound_flight_date{ get; set; } // Date of departure flight (outbound)

        [BindProperty(SupportsGet = true)]
        public string outbound_flight_time{ get; set; } // Time of departure flight (outbound)

        [BindProperty(SupportsGet = true)]
        public string inbound_flight_date{ get; set; } // Date of departure flight (inbound)

        [BindProperty(SupportsGet = true)]
        public string inbound_flight_time{ get; set; } // Time of departure flight (inbound)

        [BindProperty(SupportsGet = true)]
        public int user_ID { get; set; }


        /*
            Name: OnGet
            Date Last Updated: 4-30-2020
            Last Updated Programmer Name: Justin Tran
            Description: This function initializes the state needed for the 
                         Reservations page (Reservations.cshtml) by obtaining the selected 
                         outbound and/or inbound flight IDs and their associated flight information
        */
        public void OnGet()
        {
            // Obtain selected outbound flight ID (in type int?) from session using key "Selected_Outbound_ID"
            var try_outbound_ID = HttpContext.Session.GetInt32("Selected_Outbound_ID");
            if (try_outbound_ID != null ) outbound_ID = Convert.ToInt32(try_outbound_ID);

            // Obtain selected inbound flight ID (in type int?) from session using key "Selected_Inbound_ID"
            var try_inbound_ID = HttpContext.Session.GetInt32("Selected_Inbound_ID");
            if (try_inbound_ID != null ) inbound_ID = Convert.ToInt32(try_inbound_ID);

            // Obtain selected trip type (one-way or round trip) from session using key "Selected_Trip_Type"
            TripType = HttpContext.Session.GetString("Selected_Trip_Type");

            // Calling outbound flight information
            string temp = _reservationService.FindFlightInfo(outbound_ID);
            string[] words = temp.Split(','); //string array output 

            // Parsing the output string array back into variables (integer or string)
            outbound_route_id = Int32.Parse(words[0]);
            outbound_depart_flight_id = Int32.Parse(words[1]);
            outbound_arrival_id = Int32.Parse(words[2]);
            outbound_depart_airport_name = words[3];
            outbound_depart_city_name = words[4];
            outbound_arrival_airport_name = words[5];
            outbound_arrival_city_name = words[6];
            outbound_flight_date = words[7];
            outbound_flight_time = words[8];

            // If user chose "Round Trip", then obtain inbound flight information for displaying
            if (TripType == "T")
            {
                // Calling inbound flight information
                string temp_str = _reservationService.FindFlightInfo(inbound_ID);
                string[] flight_info = temp_str.Split(','); //string array output 

                //Parsing the output string array back into variables (integer or string)
                inbound_route_id = Int32.Parse(flight_info[0]);
                inbound_depart_flight_id = Int32.Parse(flight_info[1]);
                inbound_arrival_id = Int32.Parse(flight_info[2]);
                inbound_depart_airport_name = flight_info[3];
                inbound_depart_city_name = flight_info[4];
                inbound_arrival_airport_name = flight_info[5];
                inbound_arrival_city_name = flight_info[6];
                inbound_flight_date = flight_info[7];
                inbound_flight_time = flight_info[8];
            }
        }

        /*
            Name: OnPostConfirm
            Date Last Updated: 4-30-2020
            Last Updated Programmer Name: Justin Tran, Corey Stock, William Yung
            Description: This function is called upon the "Confirm" button click (on the modal/popup box
                         on the Reservation page), and stores the selected outbound 
                         and/or inbound flights he or she wanted via flight IDs
        */
        public IActionResult OnPostConfirm() 
        {
            // Obtain selected outbound flight ID (in type int?) from session using key "Selected_Outbound_ID"
            var try_outbound_ID = HttpContext.Session.GetInt32("Selected_Outbound_ID");
            if (try_outbound_ID != null ) outbound_ID = Convert.ToInt32(try_outbound_ID);

            // Obtain selected inbound flight ID (in type int?) from session using key "Selected_Inbound_ID"
            var try_inbound_ID = HttpContext.Session.GetInt32("Selected_Inbound_ID");
            if (try_inbound_ID != null ) inbound_ID = Convert.ToInt32(try_inbound_ID);

            // Obtain selected trip type (one-way or round trip) from session using key "Selected_Trip_Type"
            TripType = HttpContext.Session.GetString("Selected_Trip_Type");

            // Obtain user ID (in type int?) from session using key "userID"
            var try_user_ID = HttpContext.Session.GetInt32("userID");
            if (try_user_ID != null) user_ID = Convert.ToInt32(try_user_ID);

            // Create reservation for chosen outbound flight
            _reservationService.Create(FirstName, LastName, outbound_ID, inbound_ID, user_ID);
            
            // If user chose "Round trip", then create an inbound flight reservation
            if (TripType == "T")
            {
                // Create reservation for chosen inbound flight
                _reservationService.Create(FirstName, LastName, inbound_ID, outbound_ID, user_ID);
            }

            // Move user to MyTrips page after reservation and payment has been made
            return RedirectToPage("/MyTrips");
        }
    }
}







 

            