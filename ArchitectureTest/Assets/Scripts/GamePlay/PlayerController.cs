using System;
using UnityEngine;

namespace GamePlay
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float ballDeviation;
        
        private float _velocity;

        private Rigidbody2D _playerRigidBody;

        private Game _gameInstance;

        private void Start()
        {
            _playerRigidBody = GetComponent<Rigidbody2D>();
        }

        public void Constructor(float playerVelocity, Game gameInstance)
        {
            _gameInstance = gameInstance;
            _velocity = playerVelocity;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var ball = other.gameObject.GetComponent<BallController>();
            if (ball != null)
            {
                ball.Deviate(CalculateTrajectoryDeviation());
            }
        }

        private float CalculateTrajectoryDeviation()
        {
            if (Math.Abs(_playerRigidBody.velocity.x) < 0.1f)
            {
                return 0;
            }

            if (_playerRigidBody.velocity.x > 0)
            {
                return ballDeviation;
            }

            return -ballDeviation;
        }

        public void ManageMovement(bool isLeftPressed, bool isRightPressed)
        {
            if (_gameInstance != null && !_gameInstance.IsGameRunning)
            {
                return;
            }
        
            if (isLeftPressed && isRightPressed)
            {
                _playerRigidBody.velocity = Vector2.zero;
            }
            else if (isLeftPressed)
            {
                _playerRigidBody.velocity = Vector2.left * _velocity;
            }
            else if(isRightPressed)
            {
                _playerRigidBody.velocity = Vector2.right * _velocity;
            }
            else
            {
                _playerRigidBody.velocity = Vector2.zero;
            }
        }

        public void ResetPosition()
        {
            var positionStart = transform.position;
            positionStart.x = 0f;
            transform.position = positionStart;
        }
    }
}