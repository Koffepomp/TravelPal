using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.Travels;

namespace TravelPal.Accounts
{
    public class User : IUser
    {
        public List<Travel> Travels { get; set; } = new();
        public string Username { get; set; }
        public string Password { get; set; }
        public Countries Location { get; set; }

        public User(string userName, string password, Countries location)
        {
            Username = userName;
            Password = password;
            Location = location;
        }
    }
}
