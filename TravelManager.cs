using System;
using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.PackingList;
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

        public void AddTravel(
            string destination,
            Countries country,
            int travellers,
            string tripOrVacation,
            string tripType,
            bool isAllInclusive,
            DateTime selectedDate,
            List<IPackingListItem> packList
            )
        {
            Travel newTravel = new(
                destination,
                country,
                travellers,
                tripOrVacation,
                tripType,
                isAllInclusive,
                selectedDate,
                packList
                );
            Travels.Add(newTravel);
        }

        public void RemoveTravel(Travel selectedTravel)
        {
            Travels.Remove(selectedTravel);
        }


    }
}
