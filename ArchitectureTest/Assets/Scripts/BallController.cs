using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private TrailRenderer ballTrailRenderer;
    [SerializeField] private GameObject explosionParticlesGO;
    [SerializeField] private SpriteRenderer graphicSpriteRenderer;
    
    private float _speed;
    private Rigidbody2D _ballRigidbody;
    private Collider2D _ballCollider;

    private void Awake()
    {
        ballTrailRenderer.enabled = false;
        _ballRigidbody = GetComponent<Rigidbody2D>();
        _ballCollider = GetComponent<Collider2D>();
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

    public void Explode()
    {
        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
        ballTrailRenderer.Clear();
        ballTrailRenderer.enabled = false;
        _ballCollider.enabled = false;
        SetVelocity(Vector2.zero);

        FadeGraphic();
        yield return new WaitForSeconds(0.2f);
        explosionParticlesGO.SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void FadeGraphic()
    {
        var graphicColor = graphicSpriteRenderer.color;
        LeanTween.value(1, 0, 0.5f)
            .setOnUpdate(deltaAlpha =>
            {
                graphicSpriteRenderer.color =
                    new Color(graphicColor.r, graphicColor.g, graphicColor.b, deltaAlpha);
            });
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