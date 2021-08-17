using System;
using System.Collections;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private BallController Ball;
    [SerializeField] private PlayerController Player;

    [SerializeField] private BrickSpawner brickSpawner;
    [SerializeField] private LevelsProvider levelsProvider;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private DialogProvider dialogProvider;
    [SerializeField] private TextMeshProUGUI ScoreLabelTM;
    [SerializeField] private TextMeshProUGUI LivesLabelTM;
    [SerializeField] private TextMeshProUGUI GetReadyLabelTM;
    [SerializeField] private TextMeshProUGUI GetReadyCounterLabelTM;
    [SerializeField] private Death deathZone;

    public uint Score = 0;
    public uint Lives = 3;

    private uint _bricksCount;
    private bool _gameOver;
    private bool _isGameRunning;
    public bool IsGameRunning => _isGameRunning;

    private TestanoidApplication _testanoidApplication;
    private LevelScriptableObject _levelScriptableObject;

    private void Start()
    {
        _testanoidApplication = TestanoidApplication.Instance;
        
        levelsProvider.LoadLevelAsync(_testanoidApplication.CurrentLevel, OnLevelLoad, OnLevelFailToLoad);
        deathZone.Constructor(this);
    }

    private void OnLevelLoad(LevelScriptableObject level)
    {
        _levelScriptableObject = level;
        _bricksCount = _levelScriptableObject.bricksCount;
        Ball.Init(_levelScriptableObject.ballSpeed);
        playerController.Constructor(_levelScriptableObject.playerSpeed, this);
        brickSpawner.PopulateGrid(_levelScriptableObject.brickLayout, this);

        _isGameRunning = true;
        
        Reset();
    }

    private void OnLevelFailToLoad()
    {
        Navigator.Instance.Navigate(Constants.Scenes.MainMenuScene);
    }

    public void ResetBallAndPlayer()
    {
        GetReadyLabelTM.enabled = true;
        GetReadyCounterLabelTM.enabled = true;
        var pos1 = Player.transform.position;
        pos1.x = 0f;
        Player.transform.position = pos1;

        Ball.ResetPosition();

        ScoreLabelTM.text = "Score: " + Score;

        StartCoroutine(StartGame());
    }

    public void OnBallHitDeathZone()
    {
        Lives--;
        LivesLabelTM.text = "Lives: " + Lives;
        
        if (Lives == 0)
        {
            StartCoroutine(OnLose());
            _gameOver = true;
            _isGameRunning = false;
        }
        else
        {
            ResetBallAndPlayer();
        }
    }

    private void Reset()
    {
        Score = 0;
        Lives = 3;
        _bricksCount = _levelScriptableObject.bricksCount;
        
        ScoreLabelTM.text = "Score: " + Score;
        LivesLabelTM.text = "Lives: " + Lives;

        ResetBallAndPlayer();
    }

    private IEnumerator StartGame()
    {
        GetReadyCounterLabelTM.text = "3";
        yield return new WaitForSeconds(1f);
        GetReadyCounterLabelTM.text = "2";
        yield return new WaitForSeconds(1f);
        GetReadyCounterLabelTM.text = "1";
        yield return new WaitForSeconds(1f);
        GetReadyCounterLabelTM.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        GetReadyLabelTM.enabled = false;
        GetReadyCounterLabelTM.enabled = false;
        _gameOver = false;
        Ball.Kick();
    }

    public void OnScore()
    {
        Score++;
        
        ScoreLabelTM.text = "Score: " + Score;

        if (Score == _bricksCount)
        {
            StartCoroutine(OnWin());
            _gameOver = true;
            _isGameRunning = false;
        }
    }

#if UNITY_EDITOR //debug commands only work on editor
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Lives = 0;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Score = _bricksCount;
        }
    }
#endif

    private IEnumerator OnWin()
    {
        Ball.Explode();

        yield return new WaitForSeconds(1.2f);
        
        if (TestanoidApplication.Instance.CurrentLevel < levelsProvider.LevelsCount)
        {
            dialogProvider.ShowWinDialog();
        }
        else
        {
            dialogProvider.ShowGameOverDialog();
        }
    }

    private IEnumerator OnLose()
    {
        Ball.Explode();

        yield return new WaitForSeconds(1.2f);
        
        dialogProvider.ShowLoseDialog();
    }
}