using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarRentalService
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int GuitarId { get; set; }

        public string StartDate { get; set; }
        public int BookedNumberOfDays { get; set; }

        public decimal BookingCost { get; set; }

        public Booking(int bookingId, int userId, int guitarId, int bookedNumberOfDays, decimal bookingCost)
        {
            BookingId = bookingId;
            UserId = userId;
            GuitarId = guitarId;
            StartDate = DateTime.Now.ToShortDateString();
            BookedNumberOfDays = bookedNumberOfDays;
            BookingCost = bookingCost;
        }
    }
}