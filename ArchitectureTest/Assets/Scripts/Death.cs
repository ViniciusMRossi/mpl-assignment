using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private GamePlay _gamePlayInstance;
    public void Constructor(GamePlay gamePlayInstance)
    {
        _gamePlayInstance = gamePlayInstance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _gamePlayInstance.OnBallHitDeathZone();
    }
}