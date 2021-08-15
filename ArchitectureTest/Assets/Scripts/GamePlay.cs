﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    private static GamePlay _instance;
    public static GamePlay Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("GamePlay");
                _instance = go.AddComponent<GamePlay>();
            }

            return _instance;
        }
    }
    
    public BallController Ball;
    public PlayerController Player;

    [SerializeField] private LevelScriptableObject levelScriptableObject;
    [SerializeField] private BrickSpawner brickSpawner;
    public TextMeshProUGUI ScoreLabelTM;
    public TextMeshProUGUI LivesLabelTM;
    public TextMeshProUGUI GetReadyLabelTM;
    public TextMeshProUGUI GetReadyCounterLabelTM;

    public uint Score = 0;
    public uint Lives = 3;

    private uint _bricksCount;
    private bool _gameOver = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _bricksCount = levelScriptableObject.bricksCount;
        Ball.Init(levelScriptableObject.ballSpeed);
        brickSpawner.PopulateGrid(levelScriptableObject.brickLayout);
        
        Reset();
    }

    public void Goal()
    {
        GetReadyLabelTM.enabled = true;
        GetReadyCounterLabelTM.enabled = true;
        var pos1 = Player.transform.position;
        pos1.x = 0f;
        Player.transform.position = pos1;

        Ball.ResetPosition();

        ScoreLabelTM.text = "Score: " + GamePlay.Instance.Score.ToString();
        LivesLabelTM.text = "Lives: " + GamePlay.Instance.Lives.ToString();

        StartCoroutine(StartGame());
    }

    private void Reset()
    {
        Score = 0;
        Lives = 3;
        _bricksCount = levelScriptableObject.bricksCount;

        Goal();
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

    private void Update()
    {
#if true //debug commands
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Lives = 0;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Score = _bricksCount;
        }
#endif

        if (_gameOver) return;

        ScoreLabelTM.text = "Score: " + GamePlay.Instance.Score.ToString();
        LivesLabelTM.text = "Lives: " + GamePlay.Instance.Lives.ToString();

        if (Score == _bricksCount)
        {
            SceneManager.LoadScene("Win");
            _gameOver = true;
        }
        else if (Lives == 0)
        {
            SceneManager.LoadScene("Lose");
            _gameOver = true;
        }
    }
}