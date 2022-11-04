using System.Collections.Generic;
using TravelPal.Travels;

namespace TravelPal
{
    public class TravelManager
    {
        public List<Travel> Travels { get; set; } = new();

        public List<Travel> GetAllTravels()
        {
            return Travels;
        }

        public void AddTravel(Travel travel)
        {
            Travels.Add(travel);
        }

        public void RemoveTravel(Travel travel)
        {
            Travels.Remove(travel);
        }


    }
}
