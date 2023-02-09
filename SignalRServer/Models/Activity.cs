using KoalitionServer.Constants;

namespace KoalitionServer.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public int UserId { get; set; }
        public Status Status { get; set; }
        public DateTime LastRescent { get; set; }
        //to do
        //public DateTime RegistrationDay { get; set; }
        public Activity()
        {
            LastRescent = DateTime.Now;
            //RegistrationDay = DateTime.Now;
        }
    }
}
