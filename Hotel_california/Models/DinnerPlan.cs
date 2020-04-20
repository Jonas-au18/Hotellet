namespace Hotel_california.Models
{
    public class DinnerPlan
    {
        public int DinnerPlanId { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int ExpectedTotal { get; set; }
        public int ExpectedAdults { get; set; }
        public int ExpectedKids { get; set; }
        public int CheckedInTotal { get; set; }
        public int CheckedInAdults { get; set; }
        public int CheckedInKids { get; set; }
    }
}