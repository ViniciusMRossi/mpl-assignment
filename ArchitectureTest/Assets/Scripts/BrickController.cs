using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] private GameObject explosionParticlesGO;
    [SerializeField] private GameObject graphicGO;

    private Collider2D _brickCollider;
    private Rigidbody2D _brickRigidBody;

    private GamePlay _gamePlayInstance;

    public void Constructor(GamePlay gamePlayInstance)
    {
        _gamePlayInstance = gamePlayInstance;
    }

    private void Start()
    {
        _brickCollider = GetComponent<Collider2D>();
        _brickRigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Explode();
        _gamePlayInstance.OnScore();
    }

    private void Explode()
    {
        _brickCollider.enabled = false;
        _brickRigidBody.isKinematic = true;
        
        explosionParticlesGO.SetActive(true);
        graphicGO.SetActive(false);
        
    }
}