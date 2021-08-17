using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay
{
    public class BallController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private TrailRenderer ballTrailRenderer;
        [SerializeField] private GameObject explosionParticlesGO;
        [SerializeField] private SpriteRenderer graphicSpriteRenderer;
        
        [Header("Value")] 
        [Tooltip("The cosine of half the intended initial ball kick angle. A value between 0.05 and 0.1 works best")]
        [SerializeField] private float initialKickAngleVariance;
    
        private float _speed;
        private Rigidbody2D _ballRigidbody;
        private Collider2D _ballCollider;

        private void Awake()
        {
            ballTrailRenderer.enabled = false;
            _ballRigidbody = GetComponent<Rigidbody2D>();
            _ballCollider = GetComponent<Collider2D>();
        }
        private void FixedUpdate()
        {
            SetVelocity(_ballRigidbody.velocity.normalized * _speed);
        }
        public void Constructor(float speed)
        {
            _speed = speed;
        }
        
        //Applies a deviation in the ball depending on the direction the player paddle is moving
        //it avoids stagnation and emulates the behaviour of the traditional Pong
        public void Deviate(float deviation)
        {
            var velocity = _ballRigidbody.velocity;
            var newVelocityVector = new Vector2(velocity.x + deviation, velocity.y);

            if (newVelocityVector.x == 0)
            {
                newVelocityVector = new Vector2(velocity.x, velocity.y + deviation);
            }
            
            SetVelocity(newVelocityVector * _speed);
        }
        public void ApplyInitialKick()
        {
            ballTrailRenderer.enabled = true;
            SetVelocity(GenerateInitialKickAngle() * _speed);
        }

        //Used for game over
        public void Explode()
        {
            StartCoroutine(ExplodeAfterDelay());
        }
        public void ResetPosition()
        {
            ballTrailRenderer.Clear();
            ballTrailRenderer.enabled = false;
            transform.position = Vector3.zero;
            SetVelocity(Vector2.zero);
        }
        
        //The 'WaitForSeconds' are used to sync with the particle emission
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
            LeanTween.value(1f, 0f, 0.5f)
                .setOnUpdate((float deltaAlpha) =>
                {
                    graphicSpriteRenderer.color =
                        new Color(graphicColor.r, graphicColor.g, graphicColor.b, deltaAlpha);
                });
        }
        
        //Generates the angle the ball starts its drop
        private Vector2 GenerateInitialKickAngle()
        {
            var kick = new Vector2(Random.Range(-initialKickAngleVariance, initialKickAngleVariance), Random.Range(-1, -initialKickAngleVariance));
            return kick.normalized;
        }
        private void SetVelocity(Vector2 velocity)
        {
            _ballRigidbody.velocity = velocity;
        }
    }
}