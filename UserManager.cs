using System.Collections.Generic;
using TravelPal.Accounts;

namespace TravelPal
{
    public class UserManager
    {
        public List<IUser> Users { get; set; } = new();

        public IUser SignedInUser { get; set; }
        public UserManager()
        {
            // Loads the default login accounts on startup (Gandalf and Admin)
            LoadDefaultAccounts();
        }

        private void LoadDefaultAccounts()
        {
            //CreateUser("Gandalf", "asd", Countries.United_States);
            //CreateAdmin("qwe", "asd", Countries.Afghanistan);
        }

        public bool CreateUser(IUser user)
        {
            //User user = new(userName, password, country);
            //Users.Add(user);
            bool booljävel = true;
            return booljävel;
        }

        public bool UpdateUsername(IUser user, string updateName)
        {
            //gammalt namn = updateName
            bool newName = true;
            return newName;
        }

        private bool ValidateUsername(username)
        {

            return boolnått;
        }

        public bool SignInUser(username, password)
        {
            return booljapp;
        }

        //public void CreateAdmin(string userName, string password, Countries country)
        //{
        //    Admin admin = new(userName, password, country);
        //    Users.Add(admin);
        //}

        //public List<IUser> FetchAccounts()
        //{
        //    return Users;
        //}
    }
}
