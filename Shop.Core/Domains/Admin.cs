using System;
using Shop.Core.Domains.Abstract;

namespace Shop.Core.Domains {
    public class Admin : Account {
        public Admin (string name, string surname, string email, string password) : base (name, surname, email, password) {
            Role = "admin";
        }

        public void Update (string name, string surname) {
            Name = name;
            Surname = surname;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}