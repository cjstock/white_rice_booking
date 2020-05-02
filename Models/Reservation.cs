
namespace white_rice_booking
{
    public class Reservation
    {
        public string PassengerFN { get; set; }
        public string PassengerLN { get; set; }
        public int DepartFlightID { get; set; }
        public int ReturnFlightID { get; set; }
        public int UserAccountID { get; set; }
        public int ReservationID { get; set; }
    }
}