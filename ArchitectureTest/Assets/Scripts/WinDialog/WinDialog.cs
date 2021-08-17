using ApplicationTestanoid;
using Art.LeanAnimations;
using UnityEngine;

namespace WinDialog
{
    public class WinDialog : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject winLevelPanelGO;
        [SerializeField] private GameObject gameOverPanelGO;
        [SerializeField] private DialogEntryAnimation entryAnimation;

        public void ShowLevelWinDialog()
        {
            winLevelPanelGO.SetActive(true);
            gameOverPanelGO.SetActive(false);
        
            entryAnimation.Animate();
        }
    
        public void ShowGameOverDialog()
        {
            gameOverPanelGO.SetActive(true);
            winLevelPanelGO.SetActive(false);
        
            entryAnimation.Animate();
        }

        public void OnLinkedinCLick()
        {
            Application.OpenURL(Constants.Links.ViniciusLinkedin);
        }

        public void OnBackToMainClick()
        {
            Navigator.Instance.Navigate(Constants.Scenes.MainMenuScene);
        }

        public void OnNextLevelClick()
        {
            TestanoidApplication.Instance.IncrementCurrentLevel();
            Navigator.Instance.Navigate(Constants.Scenes.GamePlayScene);
        }
    }
}
