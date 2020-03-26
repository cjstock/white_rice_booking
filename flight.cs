using System;
using System.Collections.Generic;

namespace white_rice_booking
{
    public class flight
    {
        public int ID { get; set; }
        public static int MAX_PASSENGERS = 100; 
        public List<int> reservations = new List<int>(MAX_PASSENGERS);

        public string departureAirportCode { get; set; }
        public string departureAirportName { get; set; }
        public DateTime departure { get; set; }

        public string arrivalAirportCode { get; set; }
        public string arrivalAirportName { get; set; }
        public DateTime arrival { get; set; }
    }
}