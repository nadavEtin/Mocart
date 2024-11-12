using Assets.GameCore.Events;
using Assets.Scripts.Utility;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Item : MonoBehaviour, IItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }

    public void Init(string name, string description, float price)
    {
        Name = name;
        Description = description;
        Price = price;
        UpdateUI();
    }

    public void PositionItem(Vector3 position)
    {
        float offsetY = GetComponent<Renderer>().bounds.extents.y * transform.localScale.y;

        //correctly position the item model on the shelf
        transform.localPosition = position + new Vector3(0, offsetY, 0);
    }

    private void UpdateUI()
    {
        //save the initial details of the item
        EventBus.Instance.Publish(TypeOfEvent.ItemDetailsSaved, new ItemDetailsSavedEvent(Name, Description, Price));
    }
}
