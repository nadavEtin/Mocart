using Assets.Scripts.Utility;
using GameCore.Factories;
using GameCore.ScriptableObjects;
using UnityEngine;

namespace GameCore
{
    public class GameDirector : MonoBehaviour
    {
        private AssetRefs _assetRefs;

        private void Awake()
        {
            _assetRefs = Resources.Load<AssetRefs>("AssetRefs");
            _assetRefs.Init();
        }

        public void Start()
        {
            var shelfItemObjectFactory = new ShelfItemObjectFactory(_assetRefs);
            var shelf = Instantiate(_assetRefs.Shelf).GetComponent<ItemShelf>();
            shelf.Init(shelfItemObjectFactory);

            FetchServerData();
        }

        private void FetchServerData()
        {
            var webRequest = new WebRequest();
            webRequest.FetchDataAsync("https://homework.mocart.io/api/products");
        }
    }
}