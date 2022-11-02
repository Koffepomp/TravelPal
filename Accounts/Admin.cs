using TravelPal.Enums;

namespace TravelPal.Accounts
{
    public class Admin : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Countries Location { get; set; }
        public Admin(string userName, string password, Countries country)
        {
            Username = userName;
            Password = password;
            Location = country;
        }
    }
}
