using Assets.GameCore.Events;
using Assets.GameCore.UI;
using Assets.Scripts.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public struct ItemDetails
    {
        public string Name;
        public string Description;
        public float Price;

        public ItemDetails(string name, string description, float price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }

    public class UIManager : MonoBehaviour
    {
        [SerializeField] private PopupMessage _popUpMessage;
        [SerializeField] private List<ItemDetailsPanel> _itemDetailsPanels;

        private int _activeItemsIndex = 0;

        private void Awake()
        {
            EventBus.Instance.Subscribe(TypeOfEvent.ItemDetailsSaved, SaveItemDetails);
            EventBus.Instance.Subscribe(TypeOfEvent.ItemDetailsUpdated, ItemDetailsUpdated);
            EventBus.Instance.Subscribe(TypeOfEvent.Error, ErrorMessage);
        }

        //initial item details from the server
        private void SaveItemDetails(BaseEventParams eventParams)
        {
            var newDetails = (ItemDetailsSavedEvent)eventParams;
            var itemDetails = new ItemDetails(newDetails.Name, newDetails.Description, newDetails.Price);
            _itemDetailsPanels[_activeItemsIndex].gameObject.SetActive(true);
            _itemDetailsPanels[_activeItemsIndex].SetItemDetails(itemDetails);
            _activeItemsIndex++;
        }

        //user changed one of the details
        private void ItemDetailsUpdated(BaseEventParams eventParams)
        {
            var details = (ItemDetailsUpdateEvent)eventParams;
            _popUpMessage.gameObject.SetActive(true);
            _popUpMessage.PopupText.text = $"the {details.DetailChanged} was succefully changed to {details.NewValue}";
        }

        private void ErrorMessage(BaseEventParams eventParams)
        {
            var details = (ErrorEvent)eventParams;
            _popUpMessage.gameObject.SetActive(true);
            _popUpMessage.PopupText.text = details.ErrorMessage;
        }

        private void OnDestroy()
        {
            EventBus.Instance.Unsubscribe(TypeOfEvent.ItemDetailsSaved, SaveItemDetails);
            EventBus.Instance.Unsubscribe(TypeOfEvent.ItemDetailsUpdated, ItemDetailsUpdated);
            EventBus.Instance.Unsubscribe(TypeOfEvent.Error, ErrorMessage);
        }
    }
}
