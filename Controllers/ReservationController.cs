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

namespace white_rice_booking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ReservationController : ControllerBase
    {
        //Controls all actions asociated with Reservations
        int reservation_ID = 1000000; //increment by 1 for every new reservation created

        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger)
        {
            _logger = logger;
        }

        /* Create a new reservation using the flightID */
        public Boolean Create(int flightID, int userID)
        {
            Reservation newReservation = new Reservation();
            //newReservation.flightID = flightID;
            newReservation.UserAccountID = userID;
            newReservation.reservationID = reservation_ID;

            return true;
        }

        /* Use the flightID to cancel the reservation */
        public ActionResult<Reservation> Cancel(int flightID, int userID)
        {
            
            return null;
        }

        /* Create a new reservation and then cancel the old one */
        public ActionResult<Reservation> Modify(int reservationID, int flightID, int userID)
        {
            Cancel(reservationID, userID);
            Create(flightID, userID);
            return null;
        }
        //billing info

        //generate unique reservation ID

        //ask for user info
    }


}