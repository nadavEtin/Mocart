using Assets.Scripts.Utility;
using GameCore.Factories;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemShelf : MonoBehaviour, IItemShelf
{
    [SerializeField] private Transform[] _itemPositions;

    private ShelfItemObjectFactory _itemObjectFactory;
    private Dictionary<string, IItem> _shelfItems;

    public void Init(ShelfItemObjectFactory factory)
    {
        _itemObjectFactory = factory;
        _shelfItems = new Dictionary<string, IItem>();
        EventBus.Instance.Subscribe(TypeOfEvent.ItemListServerResponse, PopulateShelf);
    }

    private void PopulateShelf(BaseEventParams args)
    {
        var jsonString = ((ServerResponseEvent)args).ResponseJson;
        var itemData = JsonUtility.FromJson<Products>(jsonString);

        CreateItems(itemData);
        PositionItems();
    }

    private void CreateItems(Products itemList)
    {
        foreach (var item in itemList.products)
        {
            var newItem = _itemObjectFactory.Create(transform);
            _shelfItems.Add(item.name, newItem.GetComponent<IItem>());
            _shelfItems[item.name].Init(item.name, item.description, item.price);
        }
    }

    private void PositionItems()
    {
        var keys = _shelfItems.Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            SetBottomPosition(_shelfItems[keys[i]], _itemPositions[i].localPosition);
        }
    }

    public void SetBottomPosition(IItem item, Vector3 targetPoint)
    {
        //position items correctly on the shelf
        item.PositionItem(targetPoint);
    }

    private void OnDestroy()
    {
        EventBus.Instance.Unsubscribe(TypeOfEvent.ItemListServerResponse, PopulateShelf);
    }
}
