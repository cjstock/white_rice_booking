/*
    Name: Filter Flights Controller
    Date Last Updated: 4-18-2020
    Programmer Names: Corey Stock, William Yung
    Description: This class is the controller that will control how the website will filter the 
                 different flights for the user to view and choose from
    Important Methods/Structures/Etc: 
            Functions - FilterFlights, FilterDepartAirport, FilterArrivalAirport, FilterRoute, FilterDate
    Major Decisions: 
            - Used built in search function to look for data in the database because it keeps 
            the code cleaner and easier to use
            - Used csv files for database because we are only looking at the information
            within the database and are not updating them so there is no need for a dedicated
            database that would be more complicated 
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
using Newtonsoft.Json;

namespace white_rice_booking
{
    [ApiController]
    [Route("[controller]")]

    public class FilterFlightsController : ControllerBase
    {
        private readonly ILogger<FilterFlightsController> _logger;

        public Boolean twoway = false;
        // public string State { get; set; }

        // Departing flight variables
        public int outgoingDepartAirportCode { get; set; }
        public int outgoingArrivalAirportCode { get; set; }
        public string outgoingDepartAirportName { get; set; }
        public string outgoingArrivalAirportName { get; set; }
        public int outgoingRouteID { get; set; }

        // public DateTime outgoingDepart { get; set; }
        // public DateTime outgoingArrival { get; set; }

        // Returning flight variables
        public int incomingDepartAirportCode { get; set; }
        public int incomingArrivalAirportCode { get; set; }
        public string incomingDepartAirportName { get; set; }
        public string incomingArrivalAirportName { get; set; }
        public int incomingRouteID { get; set; }

        // public DateTime incomingDepart { get; set; }
        // public DateTime incomingArrival { get; set; }

        public FilterFlightsController(ILogger<FilterFlightsController> logger)
        {
            _logger = logger;
        }

        /*
            Name: Filter Flights
            Date Last Updated: 4-18-2020
            Last Updated Programmer Name: William Yung
            Description: This function checks if the user wants a round trip or one way flight and 
            takes in the departure & arrival airports, date leaving and if round trip date returning. 
            It then calls all the functions needed to filter for a list of viable flights
        */
        [HttpGet]
        [Route("filter/{trip_type}/{depart_loc}/{arrival_loc}/{outgoing_date}/{incoming_date}")]
        public List<Flights> FilterFlights(string trip_type, string depart_loc, string arrival_loc, 
                                    string outgoing_date, string incoming_date)
        {   
            List<Flights> outgoingList;
            List<Flights> incomingList;

            // Compares input to set type of trip as round trip or one way
            if (trip_type == "O") { 
                twoway = false;
                }

            else if (trip_type == "T"){
                twoway = true;
            } 

            // Functions called to acquire the data needed to filter the flights
            FilterDepartAirport(depart_loc);
            FilterArrivalAirport(arrival_loc);
            
            outgoingRouteID = FilterRoute(outgoingDepartAirportCode, outgoingArrivalAirportCode);
            if(twoway) incomingRouteID = FilterRoute(incomingDepartAirportCode, incomingArrivalAirportCode);

            // Gets the list of available flights based on inputs
            outgoingList = FilterDate(outgoingRouteID,outgoing_date);
            
            if(twoway){ 
                incomingList = FilterDate(incomingRouteID, incoming_date);
                return (outgoingList.Concat(incomingList).ToList());
            }
            
            return (outgoingList);

        }

        /*
            Name: Filter Depart Airport
            Date Last Updated: 4-18-2020
            Last Updated Programmer Name: William Yung
            Description: This function opens the database looks for the airport that the user wants 
            to leave from and gets the airport id from it.
        */
        [HttpGet]
        public void FilterDepartAirport(string depart_loc)
        {
            // Sets up the reader to read from the csv file
            using (var airport_reader = new StreamReader("database\\top_10_airports.csv"))
            using (var csv = new CsvReader(airport_reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Airport>();
                // Loops through the file to look for the departure airport based on user input
                foreach (var record in records)
                {
                    // Checks if input matches in database and stores the departure airport id in a variable
                    if(depart_loc == record.IATA) {
                        outgoingDepartAirportCode = record.OpenFlights_ID;
                        outgoingDepartAirportName = record.Airport_Name;
                        // If it is a round trip then store the outgoing departure airport id as
                        // incoming arrival airport id
                        if(twoway){
                            incomingArrivalAirportCode = record.OpenFlights_ID;
                            incomingArrivalAirportName = record.Airport_Name;
                        }
                    }    
                }
            }
        }

        /*
            Name: Filter Arrival Airport
            Date Last Updated: 4-18-2020
            Last Updated Programmer Name: William Yung
            Description: This function opens the database looks for the airport that the user wants 
            to arrive at and gets the airport id from it.
        */
        [HttpGet]
        public void FilterArrivalAirport(string arrival_loc)
        {
            // Sets up the reader to read from the csv file
            using (var airport_reader = new StreamReader("database\\top_10_airports.csv"))
            using (var csv = new CsvReader(airport_reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Airport>();
                // Loops through the file to look for the departure airport based on user input
                foreach (var record in records)
                {
                    // Checks if input matches in database and stores the arrival airport id in a variable
                    if(arrival_loc == record.IATA) {
                        outgoingArrivalAirportCode = record.OpenFlights_ID;
                        outgoingArrivalAirportName = record.Airport_Name;
                        // If it is a round trip then store the outgoing arrival airport id as
                        // incoming departure airport id
                        if(twoway){
                            incomingDepartAirportCode = record.OpenFlights_ID;
                            incomingDepartAirportName = record.Airport_Name;
                        }
                    }
                }
            }
        }

        /*
            Name: Filter Route
            Date Last Updated: 4-13-2020
            Last Updated Programmer Name: William Yung
            Description: This function opens the database looks for the flight route from the departure 
            airport to the arrival airport by using the ids obtained from the previous 2 functions
        */
        [HttpGet]
        public int FilterRoute(int depart_ID, int arrival_ID)
        {
            // Sets up the reader to read from the csv file
            using (var route_reader = new StreamReader("database\\top_10_routes.csv"))
            using (var csv = new CsvReader(route_reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Routes>();
                // Loops through the file to look for the departure airport based on user input
                foreach (var record in records)
                {
                    // Checks if the departure airport ID and arrival airport ID match in the 
                    // database then returns the id of the route
                    if(depart_ID == record.Source_OpenFlights_ID && arrival_ID == record.Destination_OpenFlights_ID){
                            return record.Route_ID;
                    }
                }
                return 0;
            }
        }

        /*
            Name: Filter Date
            Date Last Updated: 4-13-2020
            Last Updated Programmer Name: William Yung
            Description: This function uses the route id obtained from previous functions and the date 
            that the user wants to depart and provides a list of flights that are available that day
        */
        public List<Flights> FilterDate(int route_id, string date){
            // Sets up the reader to read from the csv file
            using (var flight_reader = new StreamReader("database\\flights.csv"))
            using (var csv = new CsvReader(flight_reader, CultureInfo.InvariantCulture))
            {
                List<Flights> flights_list = new List<Flights>();
                var records = csv.GetRecords<Flights>();
                // Loops through the file to look for the departure airport based on user input
                foreach (var record in records)
                {
                    // Checks if the departure airport ID and arrival airport ID match in the 
                    // database then returns the id of the route
                    if(route_id == record.Route_ID && date == record.Date)
                        // Stores all data that matches in a list
                        flights_list.Add(record);
                }
                return flights_list;
            }
        }
    }
}