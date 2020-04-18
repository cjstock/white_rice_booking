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
            //_reservation_ID = ;
        }

        /* Create a new reservation using the flightID */
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

        /* Use the flightID to cancel the reservation */
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

        /* Create a new reservation and then cancel the old one */
        [HttpGet]
        [Route("modify/{firstName}/{lastName}/{departID}/{returnID}/{userID}")]
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
        //billing info

        //ask for user info

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