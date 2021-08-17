using Array2DEditor;

namespace GamePlay
{
    public interface IGamePlayView
    {
        void NavigateToMainMenu();
        void ResetBallAndPlayer(uint lives);
        void LoseGame();
        void UpdateScore(uint score);
        void Win();
        void RunGame(Array2DBool brickLayout, float ballSpeed, float playerSpeed);
    }
}