namespace TravelPal.Accounts
{
    public interface IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        //public Countries Location { get; set; }
    }
}
