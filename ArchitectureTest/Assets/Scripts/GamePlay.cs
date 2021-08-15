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

    public TextMeshProUGUI ScoreLabelTM;
    public TextMeshProUGUI LivesLabelTM;
    public TextMeshProUGUI GetReadyLabelTM;
    public TextMeshProUGUI GetReadyCounterLabelTM;

    public uint Score = 0;
    public uint Lives = 3;

    uint Briks = 4;
    private bool _gameOver = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        Reset();
    }

    public void Goal()
    {
        GetReadyLabelTM.enabled = true;
        GetReadyCounterLabelTM.enabled = true;
        var pos1 = Player.transform.position;
        pos1.x = 0f;
        Player.transform.position = pos1;

        Ball.transform.position = Vector3.zero;
        Ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        ScoreLabelTM.text = "Score: " + GamePlay.Instance.Score.ToString();
        LivesLabelTM.text = "Lives: " + GamePlay.Instance.Lives.ToString();

        StartCoroutine(StartGame());
    }

    private void Reset()
    {
        Score = 0;
        Lives = 3;
        Briks = 4;

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
            Score = Briks;
        }
#endif

        if (_gameOver) return;

        ScoreLabelTM.text = "Score: " + GamePlay.Instance.Score.ToString();
        LivesLabelTM.text = "Lives: " + GamePlay.Instance.Lives.ToString();

        if (Score == Briks)
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