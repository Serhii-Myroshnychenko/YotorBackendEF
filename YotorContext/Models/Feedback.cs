using System;
using System.Collections.Generic;

#nullable disable

namespace YotorContext.Models
{
    public partial class Feedback
    {
        public Feedback()
        {
            Bookings = new HashSet<Booking>();
        }

        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string Text { get; set; }

        public virtual Customer User { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
