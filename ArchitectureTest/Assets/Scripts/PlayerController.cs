using System;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.WSA;

public class PlayerController : MonoBehaviour
{
    private float _velocity;

    private Rigidbody2D _playerRigidBody;

    private void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    public void Init(float playerVelocity)
    {
        _velocity = playerVelocity;
    }

    public void ManageMovement(bool isLeftPressed, bool isRightPressed)
    {
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