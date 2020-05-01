/*
    Name: Filter Flights
    Date Last Updated: 4-30-2020
    Programmer Names: Justin Tran, Corey Stock, William Yung
    Description: This class shall connect the front-end Index page with the Filter Flights Service 
                 back-end code, which filters and determines the different available flights
                 for the user to view and choose from.
    Important Methods/Structures/Etc: 
            Functions - OnPostFilter, OnPostChooseFlight
    Major Decisions: 
            - Used the HttpContext session for transmitting data from one PageModel to another PageModel
              since it easily allows the programmer to use a selected key value to access stored values
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
    public class FilterFlightsModel : PageModel
    {
        private readonly ILogger<FilterFlightsModel> _logger;
        private FilterFlightsService _filterflightsService; 
        public List<Flights> availableOutgoingFlights; // List of available outbound flights
        public List<Flights> availableIncomingFlights; // List of available inbound flights

        public FilterFlightsModel(ILogger<FilterFlightsModel> logger, FilterFlightsService filterflightsService)
        {
            _logger = logger;
            
            _filterflightsService = filterflightsService;
            availableOutgoingFlights = new List<Flights>(); // Empty list of available outbound flights
            availableIncomingFlights = new List<Flights>(); // Empty list of available inbound flights 
                                                            // (for roundtrips)
            
        }
        
        [BindProperty(SupportsGet = true)]
        public string DepartAirport { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string ArrivalAirport { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string depart_date { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string return_date { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string TripType { get; set; }

        [BindProperty(SupportsGet = true)]
        public int outbound_ID { get; set; }

        [BindProperty(SupportsGet = true)]
        public int inbound_ID { get; set; }


        /*
            Name: OnGet
            Date Last Updated: 4-27-2020
            Last Updated Programmer Name: Justin Tran
            Description: This function initializes state needed for the 
                         Filter Flights page (FilterFlights.cshtml)
        */
        public void OnGet()
        {
            
        }
        
        /*
            Name: OnPost
            Date Last Updated: 4-27-2020
            Last Updated Programmer Name: Justin Tran, William Yung
            Description: This function is called upon the "Submit" button click, retrieves 
                         user inputs from the form on the front-end, and determines the available
                         outbound and/or inbound flights based on user's desired choices
        */
        public IActionResult OnPostFilter() 
        {
            _filterflightsService.ClearVariables(); // Initializes all variables needed for 
                                                    // filtering flights to 0 or empty

            // This will call the back end function that obtains a list of flights to display to the user
            availableOutgoingFlights = _filterflightsService.FilterOutgoingFlights(trip_type: TripType,
                                                                   depart_loc: DepartAirport,
                                                                   arrival_loc: ArrivalAirport,
                                                                   outgoing_date: depart_date);
            
            // Calls back end function to get list of incoming flights if the user chooses a round trip
            if(TripType == "T")
                availableIncomingFlights = _filterflightsService.FilterIncomingFlights(incoming_date: return_date);
            
            // Stores user's selected trip type (if it's one-way or a round-trip)
            HttpContext.Session.SetString("Selected_Trip_Type", TripType);

            return Page(); 
            //return RedirectToPage("/Reservations");
        }

        /*
            Name: OnPostChooseFlight
            Date Last Updated: 4-29-2020
            Last Updated Programmer Name: Justin Tran, Corey Stock
            Description: This function is called upon the "Next Page" button click (on the FilterFlights page), 
                         and stores the selected outbound and/or inbound flights he or she wanted via
                         flight IDs
        */
        public IActionResult OnPostChooseFlight() 
        {
            // Saves flight IDs in session (that can be accessed later)
            // (this allows the 2 flight IDs to be passed from this PageModel to another PageModel)
            HttpContext.Session.SetInt32("Selected_Outbound_ID", outbound_ID);
            HttpContext.Session.SetInt32("Selected_Inbound_ID", inbound_ID);
            
            // Move user to Reservations page 
            return RedirectToPage("/Reservations");
        }
        
    }
}
