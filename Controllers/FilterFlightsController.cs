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

        public void FilterFlights()
        {
            FilterOutgoing();
        }

        public void FilterOutgoing()
        {
            // if (twoway)
            // {
            //     FilterIncoming( Outgoing information);
            // }

        }

        /* Defaults to reverse of Outgoing airports */
        public void FilterIncoming()
        {
                       
        }
    }
}