using System;
using System.Collections.Generic;
using TravelPal.Enums;
using TravelPal.PackingList;

namespace TravelPal.Travels
{
    public class Vacation : Travel
    {
        public bool IsAllInclusive { get; set; }

        public Vacation(string destination, Countries country, int travellers, string tripOrVacation, string tripType, bool isAllInclusive, DateTime selectedDate, List<IPackingListItem> packingList) : base(destination, country, travellers, tripOrVacation, tripType, isAllInclusive, selectedDate, packingList)
        {
            //IsAllInclusive = isAllInclusive;
        }
        public string GetInfo()
        {
            string info = $"";
            return info;
        }
    }
}
