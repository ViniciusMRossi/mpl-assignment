using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils;

namespace Providers
{
    public class LevelsProvider : MonoBehaviour
    {
        [SerializeField] private List<AssetReference> levelsAddressables;
        public int LevelsCount => levelsAddressables.Count;

        public void LoadLevelAsync(int levelIndex, Action<LevelScriptableObject> onLevelRetrieved, Action onFailToRetrieve)
        {
            AddressableLoader.LoadAssetReference(
                levelsAddressables[levelIndex], 
                onLevelRetrieved, 
                onFailToRetrieve);
        }
    }
}