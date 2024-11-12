using System.Collections.Generic;
using UnityEngine;

namespace GameCore.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AssetRefs", menuName = "Scriptable Objects/Asset References")]
    public class AssetRefs : ScriptableObject
    {
        private List<GameObject> _shelfItems;

        [SerializeField] private GameObject _shelf, _shelfItem1, _shelfItem2, _shelfItem3, _popupMsgPrefab;

        public GameObject Shelf => _shelf;
        public GameObject ShelfItem1 => _shelfItem1;
        public GameObject ShelfItem2 => _shelfItem2;
        public GameObject ShelfItem3 => _shelfItem3;
        public GameObject PopupMessagePrefab => _popupMsgPrefab;

        public void Init()
        {
            _shelfItems = new List<GameObject> { _shelfItem1, _shelfItem2, _shelfItem3 };
        }

        public GameObject GetRandomShelfItem()
        {
            //return random model
            return _shelfItems[Random.Range(0, _shelfItems.Count)];
        }
    }
}