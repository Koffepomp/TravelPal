using System;
using System.Collections.Generic;
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

        public string TripOrVacation { get; set; }
        public string TripType { get; set; }
        public bool IsAllInclusive { get; set; }
        public DateTime SelectedDate { get; set; }

        //public DateTime startDate { get; set; }
        //public DateTime endDate { get; set; }
        //public int travelDays { get; set; }

        public string GetInfo()
        {
            string info = $"{Country} for 3 days";
            return info;
        }

        public Travel(string destination, Countries country, int travellers, string tripOrVacation, string tripType, bool isAllInclusive, DateTime selectedDate, List<IPackingListItem> packingList)
        {
            Destination = destination;
            Country = country;
            Travellers = travellers;
            TripOrVacation = tripOrVacation;
            TripType = tripType;
            IsAllInclusive = isAllInclusive;
            SelectedDate = selectedDate;
            PackingList = packingList;
        }

        public int calculateTravelDays(int x, int z)
        {
            int travelDuration = x - z;
            return travelDuration;
        }

    }

}