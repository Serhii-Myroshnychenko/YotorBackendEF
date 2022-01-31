using System;
using System.Collections.Generic;

#nullable disable

namespace YotorContext.Models
{
    public partial class Organization
    {
        public Organization()
        {
            Cars = new HashSet<Car>();
            Landlords = new HashSet<Landlord>();
        }

        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Code { get; set; }
        public string Taxes { get; set; }
        public string Address { get; set; }
        public string Founder { get; set; }
        public string Account { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Landlord> Landlords { get; set; }
    }
}
