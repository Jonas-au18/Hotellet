using System.Collections.Generic;

namespace Hotel_california.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int RoomNr { get; set; }
        public int Adults { get; set; }
        public int Kids { get; set; }
    }
}