using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace white_rice_booking
{
    [ApiController]
    [Route("[controller]")]

    public class FilterFlightsController : ControllerBase
    {
        private readonly ILogger<FilterFlightsController> _logger;

        public Boolean twoway = false;
        public string State { get; set; }

        // Outgoing variables
        public string outgoingAirportCode { get; set; }
        public string outgoingAirportName { get; set; }
        public DateTime outgoingDepart { get; set; }
        public DateTime outgoingArrival { get; set; }

        // Incoming variables
        public string incomingAirportCode { get; set; }
        public string incomingAirportName { get; set; }
        public DateTime incomingDepart { get; set; }
        public DateTime incomingArrival { get; set; }


        public FilterFlightsController(ILogger<FilterFlightsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("filter/{trip_type}/{depart_loc}")]
        public String FilterFlights(string trip_type, string depart_loc)
        {   

            if (trip_type == "O") twoway = false;
            else if (trip_type == "T") twoway = true;

            //return trip_type;

            return FilterOutgoing(depart_loc);


        }

        [HttpGet]
        [Route("out_filter/{depart_loc}")]
        public String FilterOutgoing(string depart_loc)
        {
             if (twoway)
             {
                 //return FilterIncoming();
                 //return twoway;
                 if(depart_loc == "LAX") return "Los Angeles International Airport";
                 else return "No Airport Selected for round trip";  
             }
             else return "No Airport Selected for one way trip";
    
             

        }

        /* Defaults to reverse of Outgoing airports */
        public void FilterIncoming()
        {
                       
        }
    }
}