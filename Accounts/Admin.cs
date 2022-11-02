namespace TravelPal.Accounts
{
    public class Admin : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        //public Countries Location { get; set; }
        public Admin(string userName, string password, string country)
        {
            Username = userName;
            Password = password;
            Country = country;
        }
    }
}
