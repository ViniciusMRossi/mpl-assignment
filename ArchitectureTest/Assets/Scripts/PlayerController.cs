using System;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.WSA;

public class PlayerController : MonoBehaviour
{
    private float _velocity;

    private Rigidbody2D _playerRigidBody;

    private GamePlay _gamePlayInstance;

    private void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    public void Constructor(float playerVelocity, GamePlay gamePlayInstance)
    {
        _gamePlayInstance = gamePlayInstance;
        _velocity = playerVelocity;
    }

    public void ManageMovement(bool isLeftPressed, bool isRightPressed)
    {
        if (_gamePlayInstance != null && !_gamePlayInstance.IsGameRunning)
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
}