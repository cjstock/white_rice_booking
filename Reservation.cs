using System;
using System.Collections.Generic;

namespace white_rice_booking
{
    public class Reservation
    {
        public int ID { get; set; }
        public string PassengerFN { get; set; }
        public string PassengerLN { get; set; }
        public int OutboundFlightID { get; set; }
        public int InboundFlightID { get; set; }
        public int UserAccountID { get; set; }

    }
}