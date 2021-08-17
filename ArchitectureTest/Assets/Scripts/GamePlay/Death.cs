using UnityEngine;

namespace GamePlay
{
    public class Death : MonoBehaviour
    {
        private Game _gameInstance;
        public void Constructor(Game gameInstance)
        {
            _gameInstance = gameInstance;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<BallController>() != null)
            {
                _gameInstance.OnBallHitDeathZone();
            }
        }
    }
}