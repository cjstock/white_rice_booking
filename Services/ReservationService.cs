/*
    Name: Reservation Service
    Date Last Updated: 2020-04-22
    Progammer Names: Timothy Bui
    Description: This class shall handle all flight reservations, including
                 creating, deleting, and modifying reservations.
    Important Methods/Structures/Etc:
        Function - Create, Cancel, Modify
    Major Decisions: Use json files for ease of use and modifying.
*/
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IO;
using System.Text.Json;
using CsvHelper;
using white_rice_booking.Models;
using Microsoft.AspNetCore.Hosting;

namespace white_rice_booking.Services
{
    public class ReservationService
    {
        //Controls all actions asociated with Reservations
        private static int _reservation_ID = 1000000; //increment by 1 for every new reservation created

        private IEnumerable<Reservation> _reservations;

        public IWebHostEnvironment WebHostEnvironment { get; }
        
        public ReservationService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
            _reservations = GetReservations();
        }

        /*
            Name: Create
            Date Last Updated: 2020-04-22
            Last Updated Progammer Name: Timothy Bui
            Description: This function creates a reservation object that is saved to the reservations.json file.
            The file consists of a passenger's first/last name as well as the flight ID(s) and user account ID.
            Each reservation is assigned a "Reservation ID".
        */
        public IEnumerable<Reservation> Create(string firstName, string lastName, 
                        int departID, int returnID, int userID)
        {
            Reservation newReservation = new Reservation();
            newReservation.PassengerFN = firstName;
            newReservation.PassengerLN = lastName;
            newReservation.DepartFlightID = departID;
            newReservation.ReturnFlightID = returnID;
            newReservation.UserAccountID = userID;
            newReservation.ReservationID = _reservation_ID;
            _reservation_ID++;

            //Add the new reservation to the existing ones
            var reservationList = _reservations.ToList();
            reservationList.Add(newReservation);
            _reservations = reservationList.AsEnumerable();
            
            //Write to data
            //decrementSeat(departID);
            WriteToFile(JsonFileName, _reservations);
            return _reservations;
        }

        /*
            Name: Cancel
            Date Last Updated: 2020-04-22
            Last Updated Progammer: Timothy Bui
            Description: This function deletes a reservation from the reservations.json file.
            The function takes in a reservation ID and checks to see if any reservation exists
            with that given ID. If yes, that associated reservation is deleted.
        */
        public ActionResult<Reservation> Cancel(int reservationID)
        {
            foreach(var reservation in _reservations)
            {
                if(reservation.ReservationID == reservationID)
                {
                    var reservationList = _reservations.ToList();
                    reservationList.Remove(reservation);
                    _reservations = reservationList.AsEnumerable();
                    WriteToFile(JsonFileName, _reservations);
                    break;
                }
            }
            return null;
        }
        
        /*
            Name: Modify
            Date Last Updated: 2020-04-22
            Last Updated Progammer: Timothy Bui
            Description: This function creates a new reservation using info tied to an existing reservation 
            and calling "Create". Then, the original reservation is deleted using "Cancel".
        */
        public ActionResult<Reservation> Modify(int reservationID, int departID, int returnID)
        {
            string firstName = "";
            string lastName= "";
            int userID = 0;
            foreach(var reservation in _reservations)
            {
                if(reservation.ReservationID == reservationID)
                {
                    firstName = reservation.PassengerFN;
                    lastName = reservation.PassengerLN;
                    userID = reservation.UserAccountID;
                }
            }
            Create(firstName, lastName, departID, returnID, userID);
            Cancel(reservationID);
            return null;
        }
        /*
            Name: Billing
            Date Last Updated: 2020-04-22
            Last Updated Progammer: Timothy Bui
            Description: This function takes in user billing 
            information when confirming a reservation purchase.
        */
        public ActionResult<Reservation> Billing()
        {
            return null;
        }

        public void decrementSeat(int flightID)
        {
            using (var flight_reader = new StreamReader(FlightsFileName))
            {
                List<Flights> flights_list = new List<Flights>();
                int counter = 0;
                var records = System.Text.Json.JsonSerializer.Deserialize<Flights[]>(flight_reader.ReadToEnd());
                foreach (var record in records)
                {
                    if(record.Flight_ID == flightID)
                    {
                        int temp = record.Seats;
                        record.Seats = temp-1;
                        string jsonName = File.ReadAllText("flights.json");
                        dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonName);
                        jsonObj["Seats"][counter] = temp-1;
                        string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                        File.WriteAllText("flights.json", output);
                    }
                    counter++;
                }
            }
            /*var jsonName = File.ReadAllText("flights.json");
            foreach (var flight in jsonName)
            {
                if(flight.Flight_ID == flightID)
                {
                    dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonName);
                    jsonObj["Seats"][0] = "299";
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText("flights.json", output);
                }
            }*/
            /*dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            jsonObj["Seats"][0] = "299";
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("flights.json", output);*/
        }

        public void WriteToFile(string fileName, IEnumerable<object> objects)
        {
            using(var outputStream = File.OpenWrite(fileName))
            {
                JsonSerializer.Serialize<IEnumerable<object>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = false,
                        Indented = true
                    }),
                    objects
                );
            }
        }

        /*
            Name: Get Reservations
            Date Last Updated: 2020-04-22
            Last Updated Progammer: Timothy Bui
            Description: This function creates a new IEnumerable of Reservations if it doesn't exist yet.
        */
        public IEnumerable<Reservation> GetReservations()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Reservation[]>(jsonFileReader.ReadToEnd());
            }
        }

        private string FlightsFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "flights.json");}
        }

        //this property uses Path to access the database to be used by another function
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "reservations.json"); }
        }

        public string FindFlightInfo(int Flight_ID){
            int route_id = 0;
            int depart_id = 0;
            int arrival_id = 0;
            string depart_airport_name = "";
            string depart_city_name = "";
            string arrival_airport_name = "";
            string arrival_city_name = "";

            using (var flight_reader = new StreamReader(FlightsFileName))
            //using (var csv = new CsvReader(flight_reader, CultureInfo.InvariantCulture))
            {
                var records = System.Text.Json.JsonSerializer.Deserialize<Flights[]>(flight_reader.ReadToEnd());

                // Loops through the file to look for the departure airport based on user input
                foreach (var record in records)
                {
                    if(Flight_ID == record.Flight_ID){
                        route_id = record.Route_ID;
                    }
                }
            }

            using (var route_reader = new StreamReader(RouteFileName))
            using (var csv = new CsvReader(route_reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Routes>();
                // Loops through the file to look for the departure airport based on user input
                foreach (var record in records)
                {
                    // Checks if the departure airport ID and arrival airport ID match in the 
                    // database then returns the id of the route
                    if(route_id == record.Route_ID){
                            depart_id = record.Source_OpenFlights_ID;
                            arrival_id = record.Destination_OpenFlights_ID;
                    }
                }
            }

            using (var airport_reader = new StreamReader(AirportFileName))
            using (var csv = new CsvReader(airport_reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Airport>();
                // Loops through the file to look for the departure airport based on user input
                foreach (var record in records)
                {
                    // Checks if input matches in database and stores the arrival airport id in a variable
                    if(depart_id == record.OpenFlights_ID){
                        depart_airport_name = record.Airport_Name;
                        depart_city_name = record.City_Name;    
                    }

                    if(arrival_id == record.OpenFlights_ID){
                        arrival_airport_name = record.Airport_Name;
                        arrival_city_name = record.City_Name;    
                    }
                }
            }
            string output = route_id.ToString() + "," + depart_id.ToString() + "," + arrival_id.ToString() + "," + 
            depart_airport_name + "," + depart_city_name + "," + arrival_airport_name + "," + arrival_city_name;
            
            return output;
        }

        private string AirportFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "top_10_airports.csv"); }
        }

        // This property uses a path to get the routes database csv file to be used by a function
        private string RouteFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "top_10_routes.csv"); }
        }
    
    }
}
