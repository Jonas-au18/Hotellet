namespace Hotel_california.Models
{
    public class CheckIn
    {
        public int CheckInId { get; set; }
        public int RoomNr { get; set; }
        private int _Arrived { get; set; }

        public int Arrived
        {
            get { return _Arrived; }
            set { _Arrived = Adults + Kids; }
        }

        public int Adults { get; set; }
        public int Kids { get; set; }
    }
}