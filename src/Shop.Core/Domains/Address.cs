using System;
using Shop.Core.Domains.Abstract;

namespace Shop.Core.Domains {
    public class Address {
        public int Id { get; set; }
        public string FlatNumber { get; private set; }
        public string StreetNumber { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public bool Deleted { get; protected set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public Address () { }
        public Address (string flatNumber, string streetNumber,
            string street, string city, string zipCode) {
            FlatNumber = flatNumber;
            StreetNumber = streetNumber;
            Street = street;
            City = city;
            ZipCode = zipCode;
            Deleted = false;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        public void Update (string flatNumber, string streetNumber,
            string street, string city, string zipCode) {
            FlatNumber = flatNumber;
            StreetNumber = streetNumber;
            Street = street;
            City = city;
            ZipCode = zipCode;
            UpdatedAt = DateTime.UtcNow;
        }
        public void Delete () {
            if (Deleted == false) {
                Deleted = true;
                UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}