using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace white_rice_booking.Models
{
    public class UserAccount
    {
        public int ID { get; set; }

        [Required]
        public string Email { get; set; }


        // [Required, MinLength(8), MaxLength(16)]
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

        public override string ToString() => JsonSerializer.Serialize<UserAccount>(this);
    }
}