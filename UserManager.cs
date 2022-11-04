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

        }

        public bool AddUser(IUser user)
        {
            if (ValidateUsername(user.Username))
            {
                Users.Add(user);
                return true;
            }
            return false;
        }

        public bool UpdateUsername(IUser user, string updateName)
        {
            if (ValidateUsername(updateName))
            {
                user.Username = updateName;
                return true;
            }

            return false;
        }

        private bool ValidateUsername(string username)
        {
            foreach (IUser user in Users)
            {
                if (user.Username == username)
                {
                    return false;
                }
            }
            return true;
        }

        public bool SignInUser(string username, string password)
        {
            foreach (IUser user in Users)
            {
                if (user.Username == username && user.Password == password)
                {
                    SignedInUser = user;
                    return true;
                }
            }
            return false;
        }
    }
}
