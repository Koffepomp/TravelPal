using System.Collections.Generic;
using TravelPal.Accounts;

namespace TravelPal
{
    public class UserManager
    {
        List<IUser> Accounts = new();
        public UserManager()
        {
            // Loads the default login accounts on startup (Gandalf and Admin)
            LoadDefaultAccounts();
        }

        private void LoadDefaultAccounts()
        {
            CreateUser("Gandalf", "asd", "Mordor");
            CreateAdmin("qwe", "asd", "Osby");
        }

        public void CreateUser(string userName, string password, string country)
        {
            User user = new(userName, password, country);
            Accounts.Add(user);
        }

        public void CreateAdmin(string userName, string password, string country)
        {
            Admin admin = new(userName, password, country);
            Accounts.Add(admin);
        }

        public List<IUser> FetchAccounts()
        {
            return Accounts;
        }
    }
}
