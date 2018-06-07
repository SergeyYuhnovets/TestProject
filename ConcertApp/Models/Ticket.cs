namespace ConcertApp.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public Cabinet Cabinet { get; set; } // contains IDs of ConcertEvents
        //public string UserID { get; set; }
        public int ConcertEventID { get; set; }
        public ConcertEvent ConcertEvent { get; set; }
        public bool IsBought { get; set; } // bought - true, booked = false
    }
}