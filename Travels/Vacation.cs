using System;
using TravelPal.Enums;

namespace TravelPal.Travels
{
    public class Vacation : Travel
    {
        public bool IsAllInclusive { get; set; }

        public Vacation(string destination, Countries country, int travellers, string tripOrVacation, string tripType, bool isAllInclusive, DateTime selectedDate) : base(destination, country, travellers, tripOrVacation, tripType, isAllInclusive, selectedDate)
        {
            //IsAllInclusive = isAllInclusive;
        }
    }
}
