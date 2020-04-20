namespace Hotel_california.Models
{
    public class Guest
    {
        public int GuestId { get; set; }
        public int RoomNum { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public int Age { get; set; }
        private bool isAdult { get; set; }
        public bool IsAdult
        {
            get { return isAdult; }
            set
            {
                if (Age >= 13)
                {
                    isAdult = true;
                }
                else
                {
                    isAdult = false;
                }
            }
        }
    }
}