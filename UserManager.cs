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

        // Goes through all users and compares a string to existing username. Returns true if username is not taken.
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

        // Adds a user to the list of IUsers if validating returns true.
        public bool AddUser(IUser user)
        {
            if (ValidateUsername(user.Username))
            {
                Users.Add(user);
                return true;
            }
            return false;
        }

        // Updates username of a certain user, if the new username goes through validation.
        public bool UpdateUsername(IUser user, string updateName)
        {
            if (ValidateUsername(updateName))
            {
                user.Username = updateName;
                return true;
            }

            return false;
        }

        // Compares a string of username and string of password with existing accounts usernames and passwords.
        // If it finds a match with both username and password it sets that user to SignedInUser.
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
