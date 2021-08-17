using UnityEngine;
using UnityEngine.AddressableAssets;

public class DialogProvider : MonoBehaviour
{
    [SerializeField] private AssetReference winDialogAddressable;
    [SerializeField] private AssetReference loseDialogAddressable;

    public void ShowWinDialog()
    {
        AddressableLoader.LoadAssetReference(winDialogAddressable, ( GameObject dialogPrefab) =>
        {
            var dialogGO = Instantiate(dialogPrefab);
            var winDialog = dialogGO.GetComponent<WinDialog>();
            winDialog.ShowLevelWinDialog();
        }, () => { });
    }

    public void ShowGameOverDialog()
    {
        AddressableLoader.LoadAssetReference(winDialogAddressable, ( GameObject dialogPrefab) =>
        {
            var dialogGO = Instantiate(dialogPrefab);
            var winDialog = dialogGO.GetComponent<WinDialog>();
            winDialog.ShowGameOverDialog();
        }, () => { });
    }

    public void ShowLoseDialog()
    {
        AddressableLoader.LoadAssetReference(loseDialogAddressable, ( GameObject dialogPrefab) =>
        {
            var dialogGO = Instantiate(dialogPrefab);
            var loseDialog = dialogGO.GetComponent<LoseDialog>();
            loseDialog.Show();
        }, () => { });
    }
}
