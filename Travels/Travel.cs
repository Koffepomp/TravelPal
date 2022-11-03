using System;
using System.Collections.Generic;
using TravelPal.Accounts;
using TravelPal.Enums;
using TravelPal.PackingList;

namespace TravelPal.Travels
{
    public class Travel
    {
        public string Destination { get; set; }
        public Countries Country { get; set; }
        public int Travellers { get; set; }

        public List<IPackingListItem> PackingList { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TravelDays { get; set; }

        public IUser Owner { get; set; }

        public string GetInfo()
        {
            string info = $"{Country} for 3 days";
            return info;
        }
        public Travel(string destination, Countries country, int travellers, DateTime startDate, DateTime endDate, int travelDays, List<IPackingListItem> packingList, IUser owner)
        {
            Destination = destination;
            Country = country;
            Travellers = travellers;
            StartDate = startDate;
            EndDate = endDate;
            TravelDays = travelDays;
            PackingList = packingList;
            Owner = owner;
        }

        public int calculateTravelDays(int x, int z)
        {
            int travelDuration = x - z;
            return travelDuration;
        }
    }

}