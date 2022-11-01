using TravelPal.Enums;

namespace TravelPal.Travels
{
    public class Trip : Travel
    {
        public TripTypes Triptype { get; set; }

        public Trip(TripTypes triptype, string destination, Countries country, int travellers) : base(destination, country, travellers)
        {
            Triptype = triptype;

        }
    }
}
