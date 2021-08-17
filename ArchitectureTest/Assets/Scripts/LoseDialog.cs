using Art.LeanAnimations;
using DefaultNamespace;
using UnityEngine;

public class LoseDialog : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private GameObject losePanelGO;
    [SerializeField] private DialogEntryAnimation entryAnimation;

    public void Show()
    {
        losePanelGO.SetActive(true);
        
        entryAnimation.Animate();
    }

    public void OnBackToMainClick()
    {
        Navigator.Instance.Navigate(Constants.Scenes.MainMenuScene);
    }
}
