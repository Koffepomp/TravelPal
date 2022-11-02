using System;
using TravelPal.Enums;

namespace TravelPal.Travels
{
    public class Trip : Travel
    {
        public TripTypes Triptype { get; set; }

        public Trip(TripTypes triptype, string destination, Countries country, int travellers, string tripOrVacation, string tripType, bool isAllInclusive, DateTime selectedDate) : base(destination, country, travellers, tripOrVacation, tripType, isAllInclusive, selectedDate)
        {
            Triptype = triptype;

        }
    }
}
