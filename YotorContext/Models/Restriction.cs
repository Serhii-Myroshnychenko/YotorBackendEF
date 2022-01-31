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

        public int RestrictionId { get; set; }
        public int LandlordId { get; set; }
        public string CarName { get; set; }
        public string Description { get; set; }

        public virtual Landlord Landlord { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
