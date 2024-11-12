using Assets.Scripts.Utility;

namespace Assets.GameCore.Events
{
    public class ItemDetailsSavedEvent : BaseEventParams
    {

        public string Name, Description;
        public float Price;

        public ItemDetailsSavedEvent(string newNAme, string description, float price)
        {
            Name = newNAme;
            Description = description;
            Price = price;
        }
    }
}