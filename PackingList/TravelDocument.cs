namespace TravelPal.PackingList
{
    public class TravelDocument : IPackingListItem
    {
        public string name { get; set; }
        public bool isRequired { get; set; }
    }
}
