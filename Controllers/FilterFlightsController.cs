/*
    Name: Filter Flights Controller
    Date Last Updated: 4-08-2020
    Programmer Names: Corey Stock, William Yung
    Description: This class is the controller that will control how the website will filter the different
                 flights for the user to view and choose from
    Important Methods/Structures/Etc: 
            Functions - FilterFlights, FilterOutgoing, FilterIncoming
    Major Decisions: Use built in search function to look for data in the database because it keeps the code 
                     cleaner and easier to use
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CsvHelper;
using System.IO;
using System.Globalization;

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


        /*
            Name: Filter Flights
            Date Last Updated: 4-08-2020
            Last Updated Programmer Name: William Yung
            Description: This function opens the database and checks if the user wants a round trip or one way flight
        */
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
            using (var airport_reader = new StreamReader("database\\top_10_airports.csv"))
            using (var csv = new CsvReader(airport_reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Airport>();
                if (twoway)
                {
                    //return FilterIncoming();
                    //return twoway;
                    foreach (var record in records)
                    {
                        
                        if(depart_loc == record.IATA) return "Los Angeles International Airport";
                        //else return "No Airport Selected for round trip"; 
                        
                    }
                    
                }
                else return "No Airport Selected for one way trip";
            }
            return "Holder";
    
        }

        /* Defaults to reverse of Outgoing airports */
        public void FilterIncoming()
        {
                       
        }
    }
}