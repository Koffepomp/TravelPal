namespace TravelPal.PackingList
{
    public class OtherItem : IPackingListItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public OtherItem(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        // Returns a string with item quantity and item name in the packlist listview
        public string GetInfo()
        {
            return $"[{Quantity}] {Name}";
        }
    }
}
