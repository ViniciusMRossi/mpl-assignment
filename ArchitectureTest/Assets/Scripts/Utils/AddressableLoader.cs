using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Utils
{
    //This class streamlines the use of Addressables
    public static class AddressableLoader
    {
        public static void LoadAssetReference<T>(AssetReference assetReference, Action<T> onSuccess, Action onFailure)
        {
            var asyncHandle = Addressables.LoadAssetAsync<T>(assetReference);

            asyncHandle.Completed += (packageHandle) => PackageHandleCompleted(packageHandle, onSuccess, onFailure);
        }

        private static void PackageHandleCompleted<T>(AsyncOperationHandle<T> handle, Action<T> onSuccess, Action onFailure)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onSuccess.Invoke(handle.Result);
            }
            else
            {
                onFailure.Invoke();
            }
        }
    }
}
