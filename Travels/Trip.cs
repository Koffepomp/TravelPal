using System;
using System.Collections.Generic;
using TravelPal.Accounts;
using TravelPal.Enums;
using TravelPal.PackingList;

namespace TravelPal.Travels
{
    public class Trip : Travel
    {
        public TripTypes Type { get; set; }

        public Trip(TripTypes type, string destination, Countries country, int travellers, DateTime startDate, DateTime endDate, int travelDays, List<IPackingListItem> packingList, IUser owner) : base(destination, country, travellers, startDate, endDate, travelDays, packingList, owner)
        {
            Type = type;

        }
        public string GetInfo()
        {
            return $"";
        }
    }
}
