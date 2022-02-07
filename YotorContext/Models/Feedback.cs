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
        public Feedback(int user_id, string name, DateTime? date, string text)
        {
            UserId = user_id;
            Name = name;
            Date = date;
            Text = text;

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
