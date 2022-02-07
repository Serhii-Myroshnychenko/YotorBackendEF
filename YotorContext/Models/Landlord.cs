using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace YotorContext.Models
{
    public partial class Landlord
    {
        public Landlord()
        {
            Restrictions = new HashSet<Restriction>();
        }
        public Landlord(int user_id, int organization_id, string name)
        {
            UserId = user_id;
            OrganizationId = organization_id;
            Name = name;

            Restrictions = new HashSet<Restriction>();
        }

        public int LandlordId { get; set; }
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual Customer User { get; set; }
        public virtual ICollection<Restriction> Restrictions { get; set; }
    }
}
