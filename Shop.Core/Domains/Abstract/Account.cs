using System;

namespace Shop.Core.Domains.Abstract {
    public abstract class Account {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public string Role { get; protected set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool Deleted { get; private set; }
        public bool Activated { get; private set; }
        public Address Address { get; private set; }
        protected Account () { }
        public Account (string name, string surname, string email, string password) {
            Name = name;
            Surname = surname;
            Email = email;
            CreatePasswordHash (password);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Deleted = false;
            Activated = true;
        }

        public void Delete () {
            if (Deleted == false) {
                Deleted = true;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        private void CreatePasswordHash (string password) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 ()) {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
            }
        }

        public void AddAddress (Address address) {
            Address = address;
        }

    }
}