/*
    Name: Index
    Date Last Updated: 4-20-2020
    Programmer Names: Justin Tran, Corey Stock, William Yung
    Description: This class is the will connect the front end of the Filter Flights Service backend
                 code with the front end Index page
    Important Methods/Structures/Etc: N/A
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
        public List<Flights> availableFlights;

        public IndexModel(ILogger<IndexModel> logger, FilterFlightsService filterflightsService)
        {
            _logger = logger;
            _filterflightsService = filterflightsService;
            availableFlights = new List<Flights>();
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

        public void OnGet()
        {
            /*Console.WriteLine(DepartAirport);
            Console.WriteLine(ArrivalAirport);
            Console.WriteLine(TripType);
            Console.WriteLine(depart_date);
            Console.WriteLine(return_date);*/

            // This will call the back end function that obtains a list of flights to display to the user
            availableFlights = _filterflightsService.FilterFlights(TripType, DepartAirport, 
                                                                    ArrivalAirport, depart_date, return_date);
            
            foreach (var record in availableFlights){
                Console.WriteLine(record.Flight_ID);
                Console.WriteLine(record.Date);
                Console.WriteLine(record.Time);
            }
            //Console.WriteLine("Hello World");
            //Console.WriteLine();
        }
    }
}
