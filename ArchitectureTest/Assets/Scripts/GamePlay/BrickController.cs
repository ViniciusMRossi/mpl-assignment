using UnityEngine;

namespace GamePlay
{
    public class BrickController : MonoBehaviour
    {
        [SerializeField] private GameObject explosionParticlesGO;
        [SerializeField] private GameObject graphicGO;

        private Collider2D _brickCollider;
        private Rigidbody2D _brickRigidBody;

        private Game _gameInstance;

        public void Constructor(Game gameInstance)
        {
            _gameInstance = gameInstance;
        }

        private void Start()
        {
            _brickCollider = GetComponent<Collider2D>();
            _brickRigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<BallController>() == null)
            {
                return;
            }
            
            Explode();
            _gameInstance.OnScore();
        }

        private void Explode()
        {
            _brickCollider.enabled = false;
            _brickRigidBody.isKinematic = true;
        
            explosionParticlesGO.SetActive(true);
            graphicGO.SetActive(false);
        }
    }
}