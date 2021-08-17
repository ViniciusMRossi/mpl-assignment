using ApplicationTestanoid;
using Entities;
using Providers;

namespace GamePlay
{
    public class Game
    {
        private GamePlayData _gamePlayData;
        private readonly IGamePlayView _gamePlayView;

        private uint _bricksCount;
        public bool IsGameRunning { get; private set; }

        private LevelScriptableObject _levelScriptableObject;


        public Game(IGamePlayView gamePlayView, LevelsProvider levelsProvider)
        {
            _gamePlayView = gamePlayView;

            var testanoidApplication = TestanoidApplication.Instance;

            levelsProvider.LoadLevelAsync(testanoidApplication.CurrentLevel, OnLevelLoad,
                _gamePlayView.NavigateToMainMenu);
        }

        public void DebugLose()
        {
            _gamePlayData.Lives = 0;
            OnLose();
        }

        public void DebugWin()
        {
            _gamePlayData.Score = _bricksCount;

            _gamePlayView.UpdateScore(_gamePlayData.Score);

            OnWin();
        }

        private void ResetBallAndPlayer()
        {
            _gamePlayView.ResetBallAndPlayer(_gamePlayData.Lives);
        }

        public void OnBallHitDeathZone()
        {
            _gamePlayData.Lives--;

            if (_gamePlayData.Lives == 0)
            {
                OnLose();
            }
            else
            {
                _gamePlayView.ResetBallAndPlayer(_gamePlayData.Lives);
            }
        }

        private void OnLose()
        {
            IsGameRunning = false;
            _gamePlayView.LoseGame();
        }

        private void OnWin()
        {
            _gamePlayView.Win();
        }

        public void OnScore()
        {
            _gamePlayData.Score++;

            _gamePlayView.UpdateScore(_gamePlayData.Score);

            if (_gamePlayData.Score == _bricksCount)
            {
                OnWin();
            }
        }

        private void OnLevelLoad(LevelScriptableObject level)
        {
            _gamePlayData = new GamePlayData(level.livesCount, level.bricksCount);

            _levelScriptableObject = level;
            _bricksCount = _levelScriptableObject.bricksCount;

            _gamePlayView.RunGame(_levelScriptableObject.brickLayout, _levelScriptableObject.ballSpeed,
                _levelScriptableObject.playerSpeed);

            InitGame();
        }

        private void InitGame()
        {
            IsGameRunning = true;

            _gamePlayData.Score = 0;
            _gamePlayData.Lives = _levelScriptableObject.livesCount;
            _bricksCount = _levelScriptableObject.bricksCount;

            ResetBallAndPlayer();
        }
    }
}