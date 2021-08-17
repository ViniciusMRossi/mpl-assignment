using ApplicationTestanoid;
using Art.LeanAnimations;
using UnityEngine;

namespace LoseDialog
{
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
}
