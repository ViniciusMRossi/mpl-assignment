using Array2DEditor;

namespace GamePlay
{
    //interfaces for views in Unity make the code more organized, testable, and allow for flexible presentation
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