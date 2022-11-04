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
            string info = $"[{Owner.Username}]: {Country} for {TravelDays} days.";
            //if (IUser.GetType().Name == "Admin")
            //{
            //    info = $"[{Owner}]: {Country} for {TravelDays} days";
            //}
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

        // I made this based on the drawio diagram but ended up not using it
        public int calculateTravelDays(int startDate, int endDate)
        {
            int travelDuration = startDate + endDate;
            return travelDuration;
        }
    }

}