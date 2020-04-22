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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Web;
using System.Net;
using System.Net.Http;
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

        //this property uses Path to access the database to be used by another function
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "reservations.json"); }
        }

    
    }
}
