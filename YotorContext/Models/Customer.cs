using System;
using System.Collections.Generic;

#nullable disable

namespace YotorContext.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            Feedbacks = new HashSet<Feedback>();
            Landlords = new HashSet<Landlord>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public byte[] Photo { get; set; }
        public byte[] Passport { get; set; }
        public byte[] DriversLicense { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Landlord> Landlords { get; set; }
    }
}
