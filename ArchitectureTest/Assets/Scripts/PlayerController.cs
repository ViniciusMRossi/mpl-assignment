using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocity;

    private Rigidbody2D _playerRigidBody;

    private void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            StandStill();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            StandStill();
        }
    }
    public void MoveRight()
    {
        _playerRigidBody.velocity = Vector2.right * velocity;
    }
    public void MoveLeft()
    {
        _playerRigidBody.velocity = Vector2.left * velocity;
    }
    public void StandStill()
    {
        _playerRigidBody.velocity = Vector2.zero;
    }
}