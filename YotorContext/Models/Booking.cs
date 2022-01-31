using System;
using System.Collections.Generic;

#nullable disable

namespace YotorContext.Models
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int? RestrictionId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public int? FeedbackId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public int FullPrice { get; set; }
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }

        public virtual Car Car { get; set; }
        public virtual Feedback Feedback { get; set; }
        public virtual Restriction Restriction { get; set; }
        public virtual Customer User { get; set; }
    }
}
