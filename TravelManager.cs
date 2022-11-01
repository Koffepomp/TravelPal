using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.Travels;

namespace TravelPal
{
    public class TravelManager
    {
        List<Travel> Travels = new();

        public TravelManager()
        {

        }

        public List<Travel> GetAllTravels()
        {
            return Travels;
        }

        public void AddTravel(string destination, Countries country, int travellers)
        {
            Travel newTravel = new(destination, country, travellers);
            Travels.Add(newTravel);
        }

        public void RemoveTravel()
        {

        }


    }
}
