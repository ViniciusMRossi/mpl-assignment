using ApplicationTestanoid;
using UnityEngine;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            TestanoidApplication.Instance.ResetGame();
        }

        public void LoadGameScene()
        {
            Navigator.Instance.Navigate(Constants.Scenes.GamePlayScene);
        }
    }
}