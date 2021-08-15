using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    private float _speed;
    private Rigidbody2D _ballRigidbody;

    private void Awake()
    {
        _ballRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetVelocity(_ballRigidbody.velocity.normalized * _speed);
    }

    public void Init(float speed)
    {
        _speed = speed;
    }

    public void Kick()
    {
        SetVelocity(Random.insideUnitCircle * _speed);
    }

    public void ResetPosition()
    {
        transform.position = Vector3.zero;
        SetVelocity(Vector2.zero);
    }

    private void SetVelocity(Vector2 velocity)
    {
        _ballRigidbody.velocity = velocity;
    }
}