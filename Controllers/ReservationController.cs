using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace white_rice_booking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ReservationController : ControllerBase
    {
        //Controls all actions asociated with Reservations

        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger)
        {
            _logger = logger;
        }

        /* Create a new reservation using the flightID */
        public ActionResult<Reservation> Create(int flightID, int userID)
        {
            return null;
        }

        /* Use the flightID to cancel the reservation */
        public ActionResult<Reservation> Cancel(int flightID, int userID)
        {
            return null;
        }

        /* Create a new reservation and then cancel the old one */
        public ActionResult<Reservation> Modify(int flightID, int userID)
        {
            return null;
        }
    }


}