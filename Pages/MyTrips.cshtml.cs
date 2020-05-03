/*
    Name: My Trips
    Date Last Updated: 5-1-2020
    Programmer Names: Justin Tran, Corey Stock, William Yung
    Description: This class shall connect the front-end My Trips page with the Reservation Service,
                 retrieve all of the information on the flight reservations booked by the user,
                 and allow the user to modify or cancel an existing flight reservation. 
    Important Methods/Structures/Etc: 
            - Functions - OnGet, OnPostModify, OnPostCancel
    Major Decisions: N/A
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Http;
using white_rice_booking.Models;
using white_rice_booking.Services;

namespace white_rice_booking.Pages
{
    public class MyTripsModel : PageModel
    {
        private readonly ILogger<MyTripsModel> _logger;
        private ReservationService _reservationService;

        public List<Reservation> reservedFlights;

        public List<string[]> reservation_info; // Information of all reservations made by user

        public MyTripsModel(ILogger<MyTripsModel> logger, ReservationService reservationService)
        {
            _logger = logger;
            _reservationService = reservationService;

            reservedFlights = new List<Reservation>(); // Empty list of reservations made by user
            reservation_info = new List<string[]>();
        }

        [BindProperty(SupportsGet = true)]
        public int outbound_ID { get; set; } // Outbound flight ID

        [BindProperty(SupportsGet = true)]
        public int inbound_ID { get; set; } // Inbound flight ID

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

        [BindProperty(SupportsGet = true)]
        public int user_ID { get; set; }


        /*
            Name: OnGet
            Date Last Updated: 5-1-2020
            Last Updated Programmer Name: Justin Tran
            Description: This function initializes the state needed for the 
                         My Trips page (MyTrips.cshtml) by obtaining all of the information on the
                         flight reservations booked by the user. This includes the departure airport
                         name, departure city name, arrival airport name, and departure date and time.
        */
        public void OnGet()
        {
            // Obtain user ID (in type int?) from session using key "userID"
            var try_user_ID = HttpContext.Session.GetInt32("userID");
            if (try_user_ID != null) user_ID = Convert.ToInt32(try_user_ID);



            // Retrieve all reservations made by user
            using (var reservation_reader = new StreamReader(_reservationService.ReservationFileName))
            //using (var csv = new CsvReader(flight_reader, CultureInfo.InvariantCulture))
            {
                var records = System.Text.Json.JsonSerializer.Deserialize<Reservation[]>(reservation_reader.ReadToEnd());

                // Loops through the file to look for the departure airport based on user input
                foreach (var record in records)
                {
                    // Checks if the date and route id match in the database then stores the record in
                    // a list
                    if (user_ID == record.UserAccountID){
                        // Stores all data that matches in a list
                        reservedFlights.Add(record);
                    }
                }
            }
            // Using reservation ID, obtain associated Outbound ID, Inbound ID, date, 
            // time, airport names

            // Get reservation ID based on user's ID
            foreach(var record in reservedFlights)
            {
                string temp = _reservationService.FindFlightInfo(record.DepartFlightID);
                temp = temp + "," + record.ReservationID; // Append reservation ID to string
                string[] parsed_flight_info = temp.Split(','); // Store individual strings in array
                reservation_info.Add(parsed_flight_info);
            }


        } 

        /*
            Name: OnPostModify
            Date Last Updated: 5-1-2020
            Last Updated Programmer Name: Justin Tran, Corey Stock, William Yung
            Description: This function is called upon the "Modify" button click (on the My Trips 
                        page), and modifies the user's existing flight reservation by creating
                        a new reservation using the user information tied to an existing reservation
                        and then deleting the original reservation
        */
        public IActionResult OnPostModify() 
        {
            // Deletes old reservation and creates a new reservation for the user
            _reservationService.Modify(reservation_ID, outbound_ID, inbound_ID);

            //return Page();

            // Moves user to Modify Flights page
            return RedirectToPage("/ModifyFlight");
        }

        /*
            Name: OnPostCancel
            Date Last Updated: 5-1-2020
            Last Updated Programmer Name: Justin Tran, William Yung
            Description: This function is called upon the "Cancel" button click (on the My Trips page), 
                         and cancels the desired reserved flight based on user ID and reservation ID
        */
        public IActionResult OnPostCancel(int res_ID) 
        {    
            // Cancel reservation
            _reservationService.Cancel(res_ID);

            // Move user back to My Trips page
            return RedirectToPage("/MyTrips");
        }

    }
}
