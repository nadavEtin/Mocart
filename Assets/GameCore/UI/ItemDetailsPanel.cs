using Assets.GameCore.Events;
using Assets.Scripts.Utility;
using GameCore.UI;
using TMPro;
using UnityEngine;

namespace Assets.GameCore.UI
{
    public class ItemDetailsPanel : MonoBehaviour, IItemDetailsPanel
    {
        [SerializeField] private TMP_InputField _nameInput, _priceInput;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        private ItemDetails _currentDetails;

        private void Start()
        {
            _nameInput.onEndEdit.AddListener(NameChanged);
            _priceInput.onEndEdit.AddListener(PriceChanged);
        }

        public void SetItemDetails(ItemDetails itemDetails)
        {
            _nameInput.text = itemDetails.Name;
            _descriptionText.text = itemDetails.Description;
            _priceInput.text = itemDetails.Price.ToString();

            _currentDetails = itemDetails;
        }

        private void NameChanged(string newNAme)
        {
            //show popup if the value was changed
            if (newNAme != _currentDetails.Name)
            {
                _currentDetails.Name = newNAme;
                EventBus.Instance.Publish(TypeOfEvent.ItemDetailsUpdated, new ItemDetailsUpdateEvent("name", newNAme));
            }
        }

        private void PriceChanged(string newPrice)
        {
            float f;

            //validate user input is a float value
            if (float.TryParse(newPrice, out f))
            {
                if (f != _currentDetails.Price)
                {
                    _currentDetails.Price = f;
                    var newPriceText = f.ToString();
                    _priceInput.text = newPriceText;
                    EventBus.Instance.Publish(TypeOfEvent.ItemDetailsUpdated, new ItemDetailsUpdateEvent("price", newPriceText));
                }
            }
            else
            {
                EventBus.Instance.Publish(TypeOfEvent.Error, new ErrorEvent("Please enter a valid float value"));
                _priceInput.text = _currentDetails.Price.ToString();
            }
        }
    }
}