using System;
using System.Collections.Generic;
using TravelPal.Accounts;
using TravelPal.Enums;
using TravelPal.PackingList;

namespace TravelPal.Travels
{
    public class Vacation : Travel
    {
        public bool IsAllInclusive { get; set; }

        public Vacation(bool isAllInclusive, string destination, Countries country, int travellers, DateTime startDate, DateTime endDate, int travelDays, List<IPackingListItem> packingList, IUser owner) : base(destination, country, travellers, startDate, endDate, travelDays, packingList, owner)
        {
            IsAllInclusive = isAllInclusive;
        }
        public string GetInfo()
        {
            string info = $"";
            return info;
        }
    }
}
