using System;
using System.Collections.Generic;

#nullable disable

namespace YotorContext.Models
{
    public partial class Car
    {
        public Car()
        {
            Bookings = new HashSet<Booking>();
        }

        public Car(int organization_id, string model, string brand, string year, string transmission, string address, bool status, string type, int price, string photo, string description, string number)
        {
            OrganizationId = organization_id;
            Model = model;
            Brand = brand;
            Year = year;
            Transmission = transmission;
            Address = address;
            Status = status;
            Type = type;
            Price = price;
            Photo = photo;
            Description = description;
            Number = number;

            Bookings = new HashSet<Booking>();
        }

        public int CarId { get; set; }
        public int OrganizationId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Year { get; set; }
        public string Transmission { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
