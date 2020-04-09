/*
    Name: Filter Flights Controller
    Date Last Updated: 4-09-2020
    Programmer Names: Corey Stock, William Yung
    Description: This class is the controller that will control how the website 
                 will filter the different flights for the user to view and 
                 choose from
    Important Methods/Structures/Etc: 
            Functions - FilterFlights, FilterOutgoing, FilterIncoming
    Major Decisions: Use built in search function to look for data in the 
                     database because it keeps the code cleaner and easier 
                     to use
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
            Date Last Updated: 4-09-2020
            Last Updated Programmer Name: William Yung
            Description: This function checks if the user wants a round trip or 
                         one way flight and takes in the departure and arrival 
                         location for other functions to use
        */
        [HttpGet]
        [Route("filter/{trip_type}/{depart_loc}/{arrival_loc}")]
        public String FilterFlights(string trip_type, string depart_loc, 
                                    string arrival_loc)
        {   
            String t_type = NULL;

            // Compares input to set type of trip as round trip or one way
            if (trip_type == "O") { 
                twoway = false;
                t_type = "One Way Trip";
                }

            else if (trip_type == "T"){
                twoway = true;
                t_type = "Round Trip";
            } 

            return "Trip Type: " + t_type + "\n" 
                        + FilterOutgoingAirport(depart_loc, arrival_loc);
        }

        /*
            Name: Filter Outgoing Airport
            Date Last Updated: 4-09-2020
            Last Updated Programmer Name: William Yung
            Description: This function opens the database looks for the airport 
                         that the user wants to leave from and gets the airport 
                         id from it.
        */
        [HttpGet]
        public String FilterOutgoingAirport(string depart_loc, 
                                            string arrival_loc)
        {
            // Sets up the reader to read from the csv file
            using (var airport_reader = new StreamReader("database\\top_10_airports.csv"))
            using (var csv = new CsvReader(airport_reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Airport>();
                // Loops through the file to look for the departure airport 
                // based on user input
                foreach (var record in records)
                {
                    // Checks if input matches in database and return the departure airport 
                    // information and then calls the function to get the arrival airport information
                    if(depart_loc == record.IATA) 
                    return "\n\nDeparture\nAirport ID: " 
                            + record.OpenFlights_ID + "\nAirport Name: " 
                            + record.Airport_Name + "\nCity Name: " 
                            + record.City_Name + "\n" 
                            + FilterOutgoingArrivalAirport(record.OpenFlights_ID, arrival_loc);     
                }
                return "Incorrect Name Entered";
            }
        }

        /*
            Name: Filter Outgoing Arrival Airport
            Date Last Updated: 4-09-2020
            Last Updated Programmer Name: William Yung
            Description: This function opens the database looks for the airport 
                         that the user wants to arrive at and gets the airport 
                         id from it.
        */
        [HttpGet]
        public String FilterOutgoingArrivalAirport(int depart_loc_ID, string arrival_loc)
        {
            // Sets up the reader to read from the csv file
            using (var airport_reader = new StreamReader("database\\top_10_airports.csv"))
            using (var csv = new CsvReader(airport_reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Airport>();
                // Loops through the file to look for the departure airport 
                // based on user input
                foreach (var record in records)
                {
                    // Checks if input matches in database and if it is a round trip or one way trip
                    // and returns the arrival airport information, then calls the function
                    // to get the flight route information, and then calls the function to get
                    // the return trip route information if it is a round trip 
                    if(arrival_loc == record.IATA && twoway) 
                    return "\n\nArrival\nAirport ID: " + record.OpenFlights_ID 
                            + "\nAirport Name: " + record.Airport_Name 
                            + "\nCity Name: " + record.City_Name + "\n\nOutgoing " 
                            + FilterRoute(depart_loc_ID, record.OpenFlights_ID) + "\n"
                            + FilterIncoming(record.OpenFlights_ID, depart_loc_ID); 
                    
                    else if(arrival_loc == record.IATA && !twoway) 
                    return "\n\nArrival\nAirport ID: " + record.OpenFlights_ID 
                            + "\nAirport Name: " + record.Airport_Name + "\nCity Name: " 
                            + record.City_Name + "\n\nOutgoing " 
                            + FilterRoute(depart_loc_ID, record.OpenFlights_ID);   
                }
                return "Incorrect Name Entered";
            }
        }

        /*
            Name: Filter Route
            Date Last Updated: 4-09-2020
            Last Updated Programmer Name: William Yung
            Description: This function opens the database looks for the flight 
                         route from the departure airport to the arrival airport 
                         by using the ids obtained from the previous 2 functions
        */
        [HttpGet]
        public String FilterRoute(int depart_loc_ID, int arrival_loc_ID)
        {
            // Sets up the reader to read from the csv file
            using (var route_reader = new StreamReader("database\\top_10_routes.csv"))
            using (var csv = new CsvReader(route_reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Routes>();
                // Loops through the file to look for the departure airport 
                // based on user input
                foreach (var record in records)
                {
                    // Checks if the departure airport ID and arrival airport ID match in the 
                    // database then returns the id of the route
                    if(depart_loc_ID == record.Source_OpenFlights_ID 
                        && arrival_loc_ID == record.Destination_OpenFlights_ID)
                    return "Route\nRoute ID: " + record.Route_ID + "\nDeparture: " 
                            + record.Source_IATA + "\nArrival: " + record.Destination_IATA;
                }
                return "Route Does Not Exist";
            }
        }

        /*
            Name: Filter Incoming
            Date Last Updated: 4-09-2020
            Last Updated Programmer Name: William Yung
            Description: This function calls the Filter Route function to get 
                         the route id for the return trip
        */
        [HttpGet]
        public String FilterIncoming(int depart_loc_ID, int arrival_loc_ID)
        {
            // Calls the Filter Route function with the departure and arrival ids flipped to look 
            // for the return route id  
            return "\nReturning " + FilterRoute(depart_loc_ID, arrival_loc_ID);
        }
    }
}