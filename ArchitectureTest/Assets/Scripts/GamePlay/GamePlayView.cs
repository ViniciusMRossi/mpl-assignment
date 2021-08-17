using System.Collections;
using ApplicationTestanoid;
using Array2DEditor;
using Providers;
using TMPro;
using UnityEngine;

namespace GamePlay
{
    public class GamePlayView : MonoBehaviour, IGamePlayView
    {
        [Header("References")]
        [SerializeField] private BallController ballController;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private BrickSpawner brickSpawner;
        [SerializeField] private TextMeshProUGUI scoreLabelTM;
        [SerializeField] private TextMeshProUGUI livesLabelTM;
        [SerializeField] private TextMeshProUGUI readyLabelTM;
        [SerializeField] private TextMeshProUGUI readyCounterLabelTM;
        [SerializeField] private Death deathZone;
    
        [Header("Data")]
        [SerializeField] private LevelsProvider levelsProvider;
        [SerializeField] private DialogProvider dialogProvider;

        private Game _game;

        private void Start()
        {
            _game = new Game(this, levelsProvider);

            deathZone.Constructor(_game);
        }

        private IEnumerator StartGame()
        {
            readyCounterLabelTM.text = "3";
            yield return new WaitForSeconds(1f);
            readyCounterLabelTM.text = "2";
            yield return new WaitForSeconds(1f);
            readyCounterLabelTM.text = "1";
            yield return new WaitForSeconds(1f);
            readyCounterLabelTM.text = "GO!";
            yield return new WaitForSeconds(0.5f);

            readyLabelTM.enabled = false;
            readyCounterLabelTM.enabled = false;
            ballController.Kick();
        }
        private IEnumerator OnWin()
        {
            ballController.Explode();

            yield return new WaitForSeconds(1.2f);
        
            if (TestanoidApplication.Instance.CurrentLevel + 1 < levelsProvider.LevelsCount)
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
            livesLabelTM.text = "Lives: 0";
        
            ballController.Explode();

            yield return new WaitForSeconds(1.2f);
        
            dialogProvider.ShowLoseDialog();
        }
    
        public void OnDebugWin()
        {
            _game.DebugWin();
        }

        public void OnDebugLose()
        {
            _game.DebugLose();
        }

        #region IGamePlayView implementation
        public void NavigateToMainMenu()
        {
            Navigator.Instance.Navigate(Constants.Scenes.MainMenuScene);
        }

        public void ResetBallAndPlayer(uint lives)
        {
            readyLabelTM.enabled = true;
            readyCounterLabelTM.enabled = true;
        
            playerController.ResetPosition();
            ballController.ResetPosition();

            livesLabelTM.text = $"Lives: {lives}";

            StartCoroutine(StartGame());
        }

        public void LoseGame()
        {
            StartCoroutine(OnLose());
        }

        public void UpdateScore(uint score)
        {
            scoreLabelTM.text = $"Score: {score}";
        }

        public void Win()
        {
            StartCoroutine(OnWin());
        }

        public void RunGame(Array2DBool brickLayout, float ballSpeed, float playerSpeed)
        {
            ballController.Constructor(ballSpeed);
            playerController.Constructor(playerSpeed, _game);
            brickSpawner.PopulateGrid(brickLayout, _game);
        }
        #endregion
    }
}