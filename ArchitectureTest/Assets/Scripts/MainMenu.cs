using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGameScene()
    {
        Navigator.Instance.Navigate(Constants.Scenes.GamePlayScene);
    }
}