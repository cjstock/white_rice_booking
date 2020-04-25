/*
    Name: Index
    Date Last Updated: 4-23-2020
    Programmer Names: Justin Tran, Corey Stock, William Yung
    Description: This class is the will connect the front end of the Filter Flights Service backend
                 code with the front end Index page
    Important Methods/Structures/Etc: OnGet
    Major Decisions: N/A
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using white_rice_booking.Controllers;
using white_rice_booking.Models;
using white_rice_booking.Services;

namespace white_rice_booking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private FilterFlightsService _filterflightsService;
        public List<Flights> availableOutgoingFlights;
        public List<Flights> availableIncomingFlights;

        public IndexModel(ILogger<IndexModel> logger, FilterFlightsService filterflightsService)
        {
            _logger = logger;
            _filterflightsService = filterflightsService;
            availableOutgoingFlights = new List<Flights>();
            availableIncomingFlights = new List<Flights>();
        }

        [BindProperty(SupportsGet = true)]
        public string DepartAirport{get; set;}
        
        [BindProperty(SupportsGet = true)]
        public string ArrivalAirport{get; set;}
        
        [BindProperty(SupportsGet = true)]
        public string depart_date{get;set;}
        
        [BindProperty(SupportsGet = true)]
        public string return_date{get;set;}
        
        [BindProperty(SupportsGet = true)]
        public string TripType{get;set;}

        [BindProperty(SupportsGet = true)]
        public int OutboundFlightID{get;set;}

        [BindProperty(SupportsGet = true)]
        public int InboundFlightID{get;set;}


        public void OnGet()
        {
            _filterflightsService.ClearVariables();
            // This will call the back end function that obtains a list of flights to display to the user
            availableOutgoingFlights = _filterflightsService.FilterOutgoingFlights(trip_type: TripType,
                                                                   depart_loc: DepartAirport,
                                                                   arrival_loc: ArrivalAirport,
                                                                   outgoing_date: depart_date);
            
            // Calls back end function to get list of incoming flights if the user chooses a round trip
            if(TripType == "T")
                availableIncomingFlights = _filterflightsService.FilterIncomingFlights(incoming_date: return_date);
        }
    }
}
