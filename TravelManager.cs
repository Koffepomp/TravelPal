using System.Collections.Generic;
using TravelPal.Travels;

namespace TravelPal
{
    public class TravelManager
    {
        public List<Travel> Travels { get; set; } = new();

        //public List<Travel> GetAllTravels()
        //{
        //    return Travels;
        //}

        //public void AddTravel(string destination, Countries country, int travellers, string tripOrVacation, string tripType, bool isAllInclusive, DateTime selectedDate, List<IPackingListItem> packList)
        //{
        //    Travel newTravel = new(destination,country,travellers,tripOrVacation,tripType,isAllInclusive,selectedDate,packList);
        //    Travels.Add(newTravel);
        //}

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
