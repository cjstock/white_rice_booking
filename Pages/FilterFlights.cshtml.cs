/*
    Name: Index
    Date Last Updated: 4-27-2020
    Programmer Names: Justin Tran, Corey Stock, William Yung
    Description: This class connects the front-end Index page with the Filter Flights Service 
                 back-end code 
    Important Methods/Structures/Etc: OnPost
    Major Decisions: N/A
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

namespace white_rice_booking.Pages
{
    public class FilterFlightsModel : PageModel
    {
        private readonly ILogger<FilterFlightsModel> _logger;
        private FilterFlightsService _filterflightsService;
        public List<Flights> availableOutgoingFlights;
        public List<Flights> availableIncomingFlights;

        public FilterFlightsModel(ILogger<FilterFlightsModel> logger, FilterFlightsService filterflightsService)
        {
            _logger = logger;
            
            _filterflightsService = filterflightsService;
            availableOutgoingFlights = new List<Flights>();
            availableIncomingFlights = new List<Flights>();
            
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
            
            //return Page();
        }

        /*
            Name: OnPost
            Date Last Updated: 4-27-2020
            Last Updated Programmer Name: Justin Tran, William Yung
            Description: This function is called upon the "Submit" button click, retrieves 
                         user inputs from the form on the front-end, and determines the available
                         outbound and/or inbound flights based on user's desired choices
        */
        public IActionResult OnPost() 
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

            return Page(); 

            //return RedirectToPage("/ChosenFlight");
        }
// NOTE TO SELF: (DELETE LATER)
        /* AFTER user selects a flight (which is listed with a price in $), REDIRECT user to CHOSEN FLIGHT PAGE */
    }
}
