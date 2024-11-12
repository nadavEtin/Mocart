using Assets.Scripts.Utility;

public class ItemDetailsUpdateEvent : BaseEventParams
{
    public string DetailChanged, NewValue;

    public ItemDetailsUpdateEvent(string detailChanged, string newValue)
    {
        DetailChanged = detailChanged;
        NewValue = newValue;
    }
}
