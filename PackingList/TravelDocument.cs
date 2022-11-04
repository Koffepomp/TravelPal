namespace TravelPal.PackingList
{
    public class TravelDocument : IPackingListItem
    {
        public string Name { get; set; }
        public bool IsRequired { get; set; }

        public TravelDocument(string name, bool isRequired)
        {
            Name = name;
            IsRequired = isRequired;
        }

        // Returns a required string after an item name if required checkbox is checked
        public string GetInfo()
        {
            if (IsRequired)
                return $"{Name} [REQUIRED]";
            else
                return $"{Name}";
        }
    }
}
