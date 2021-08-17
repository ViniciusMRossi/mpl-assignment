using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils;

namespace Providers
{
    public class DialogProvider : MonoBehaviour
    {
        [Tooltip("Asset reference of the WinDialogPrefab.")]
        [SerializeField] private AssetReference winDialogAddressable;
        [Tooltip("Asset reference of the LoseDialogPrefab.")]
        [SerializeField] private AssetReference loseDialogAddressable;

        public void ShowWinDialog()
        {
            AddressableLoader.LoadAssetReference(winDialogAddressable, ( GameObject dialogPrefab) =>
            {
                var dialogGO = Instantiate(dialogPrefab);
                var winDialog = dialogGO.GetComponent<WinDialog.WinDialog>();
                winDialog.ShowLevelWinDialog();
            }, () => { });
        }

        public void ShowGameOverDialog()
        {
            AddressableLoader.LoadAssetReference(winDialogAddressable, ( GameObject dialogPrefab) =>
            {
                var dialogGO = Instantiate(dialogPrefab);
                var winDialog = dialogGO.GetComponent<WinDialog.WinDialog>();
                winDialog.ShowGameOverDialog();
            }, () => { });
        }

        public void ShowLoseDialog()
        {
            AddressableLoader.LoadAssetReference(loseDialogAddressable, ( GameObject dialogPrefab) =>
            {
                var dialogGO = Instantiate(dialogPrefab);
                var loseDialog = dialogGO.GetComponent<LoseDialog.LoseDialog>();
                loseDialog.Show();
            }, () => { });
        }
    }
}
