using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

/*
    Name: Index
    Date Last Updated: 4-23-2020
    Programmer Names: Justin Tran, Corey Stock, William Yung
    Description: This class connects the home page front-end, namely Index.cshtml, with the 
                 initial page loading and page navigation controls in the back-end
    Important Methods/Structures/Etc: OnPost
    Major Decisions: N/A
*/

namespace white_rice_booking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
/*        
        private FilterFlightsService _filterflightsService;
        public List<Flights> availableOutgoingFlights;
        public List<Flights> availableIncomingFlights;
*/        
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
/*            
            _filterflightsService = filterflightsService;
            availableOutgoingFlights = new List<Flights>();
            availableIncomingFlights = new List<Flights>();
*/            
        }
/*
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
*/
        public void OnGet() 
        {

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("loggedInUser");
            return RedirectToPage("Index");
        }
        
        public IActionResult OnPost() 
        {
/*            
            _filterflightsService.ClearVariables();
            // This will call the back-end function that obtains a list of flights to display to the user
            availableOutgoingFlights = _filterflightsService.FilterOutgoingFlights(trip_type: TripType,
                                                                   depart_loc: DepartAirport,
                                                                   arrival_loc: ArrivalAirport,
                                                                   outgoing_date: depart_date);
            
            // Calls back-end function to get list of incoming flights if the user chooses a round trip
            if(TripType == "T")
                availableIncomingFlights = _filterflightsService.FilterIncomingFlights(incoming_date: return_date);
            return Page();
*/            
            return RedirectToPage("/FilterFlights");
            
        }

    }
}
