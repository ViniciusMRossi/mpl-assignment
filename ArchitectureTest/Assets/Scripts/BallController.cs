using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private TrailRenderer ballTrailRenderer;
    
    private float _speed;
    private Rigidbody2D _ballRigidbody;

    private void Awake()
    {
        ballTrailRenderer.enabled = false;
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
        ballTrailRenderer.enabled = true;
        SetVelocity(GenerateInitialKickAngle() * _speed);
    }

    private Vector2 GenerateInitialKickAngle()
    {
        const float sin45 = 0.7f;
        var kick = new Vector2(Random.Range(-sin45, sin45), Random.Range(-1, -sin45));
        return kick.normalized;
    }

    public void ResetPosition()
    {
        ballTrailRenderer.Clear();
        ballTrailRenderer.enabled = false;
        transform.position = Vector3.zero;
        SetVelocity(Vector2.zero);
    }

    private void SetVelocity(Vector2 velocity)
    {
        _ballRigidbody.velocity = velocity;
    }
}