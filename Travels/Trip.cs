using System;
using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.PackingList;

namespace TravelPal.Travels
{
    public class Trip : Travel
    {
        public TripTypes Triptype { get; set; }

        public Trip(TripTypes triptype, string destination, Countries country, int travellers, string tripOrVacation, string tripType, bool isAllInclusive, DateTime selectedDate, List<IPackingListItem> packingList) : base(destination, country, travellers, tripOrVacation, tripType, isAllInclusive, selectedDate, packingList)
        {
            Triptype = triptype;

        }
    }
}
