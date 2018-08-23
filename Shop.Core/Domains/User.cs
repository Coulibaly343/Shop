using Shop.Core.Domains.Abstract;

namespace Shop.Core.Domains {
    public class User : Account {
        public User (string name, string surname, string email, string password) : base (name, surname, email, password) {
            Role = "user";
        }

        public void Update (string name, string surname) {
            Name = name;
            Surname = surname;
        }
    }
}