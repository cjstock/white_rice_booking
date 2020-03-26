using System;
using System.Collections.Generic;

namespace white_rice_booking
{
    public class UserAccount
    {
        public int ID { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<int> ReservationIDs { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public int CardNumber { get; set; }
        public int SecurityCode { get; set; }
        public string CardFN { get; set; }
        public string CardLN { get; set; }
    }
}