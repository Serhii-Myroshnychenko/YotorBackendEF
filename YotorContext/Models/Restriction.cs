using System;
using System.Collections.Generic;

#nullable disable

namespace YotorContext.Models
{
    public partial class Restriction
    {
        public Restriction()
        {
            Bookings = new HashSet<Booking>();
        }
        public Restriction(int landlord_id, string car_name, string description)
        {
            LandlordId = landlord_id;
            CarName = car_name;
            Description = description;

            Bookings = new HashSet<Booking>();
        }

        public int RestrictionId { get; set; }
        public int LandlordId { get; set; }
        public string CarName { get; set; }
        public string Description { get; set; }

        public virtual Landlord Landlord { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
