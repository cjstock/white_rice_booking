/*
    Name: Reservation Controller
    Date Last Updated: 2020-04-18
    Progammer Names: Timothy Bui
    Description: This class is the controller that will handle all flight reservations,
                 including creating, deleting, and modifying reservations.
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
using CsvHelper;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;

namespace white_rice_booking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ReservationController : ControllerBase
    {
        //Controls all actions asociated with Reservations
        private static int _reservation_ID = 1000000; //increment by 1 for every new reservation created
        private static List<Reservation> _reservations;
        private string _db = @"database/reservations.json";


        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger)
        {
            _logger = logger;
            _reservations = GetReservations(_db);
        }

        /*
            Name: Create
            Date Last Updated: 2020-04-18
            Last Updated Progammer Name: Timothy Bui
            Description: This function creates a reservation object that is saved to the reservations.json file.
            The file consists of a passenger's first/last name as well as the flight ID(s) and user account ID.
            Each reservation is assigned a "Reservation ID".
        */
        [HttpGet]
        [Route("create/{firstName}/{lastName}/{departID}/{returnID}/{userID}")]
        public List<Reservation> Create(string firstName, string lastName, 
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
            _reservations.Add(newReservation);

            //Write modified _reservations to the database
            string json = GetDBasString(_db);
            System.IO.File.WriteAllText(_db, json);

            return _reservations;
        }

        /*
            Name: Cancel
            Date Last Updated: 2020-04-18
            Last Updated Progammer: Timothy Bui
            Description: This function deletes a reservation from the reservations.json file.
            The function takes in a reservation ID and checks to see if any reservation exists
            with that given ID. If yes, that associated reservation is deleted.
        */
        [HttpGet]
        [Route("cancel/{reservationID}")]
        public ActionResult<Reservation> Cancel(int reservationID)
        {
            foreach(var reservation in _reservations)
            {
                if(reservation.ReservationID == reservationID)
                {
                    _reservations.Remove(reservation);
                    string json = GetDBasString(_db);
                    System.IO.File.WriteAllText(_db, json);
                    break;
                }
            }
            return null;
        }

        /*
            Name: Modify
            Date Last Updated: 2020-04-18
            Last Updated Progammer: Timothy Bui
            Description: This function creates a new reservation using info tied to an existing reservation 
            and calling "Create". Then, the original reservation is deleted using "Cancel".
        */
        [HttpGet]
        [Route("modify/{reservationID}/{departID}/{returnID}")]
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

        //TODO: billing information, user information, seat counter on flights
        public ActionResult<Reservation> BillingInfo()
        {
            return null;
        }

        /*
            Name: Get Reservations
            Date Last Updated: 2020-04-18
            Last Updated Progammer: Timothy Bui
            Description: This function creates a new list of Reservations if it doesn't exist yet.
        */
        private static List<Reservation> GetReservations(string path)
        {
            string reservations = System.IO.File.ReadAllText(path);
            List<Reservation> reservation_list = JsonConvert.DeserializeObject<List<Reservation>>(reservations);

            if (reservation_list == null)
            {
                reservation_list = new List<Reservation>();
            }

            return reservation_list;
        }

        private static string GetDBasString(string path)
        {
            return @JsonConvert.SerializeObject(_reservations);
        }
    }
}