using GameCore.ScriptableObjects;
using UnityEngine;

namespace GameCore.Factories
{
    public enum ItemTypes
    {
        GenericShelfItem
    }

    public class ShelfItemObjectFactory : BaseGameObjectFactory
    {
        private AssetRefs _assetRefs;

        public ShelfItemObjectFactory(AssetRefs assetRefs)
        {
            _assetRefs = assetRefs;
        }

        public override GameObject Create(Transform parent = null)
        {
            var obj = Object.Instantiate(_assetRefs.GetRandomShelfItem());
            if (parent != null)
                obj.transform.parent = parent;

            return obj;
        }
    }
}