using UnityEngine;

public class GameInputManager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private PlayerController playerController;
    
    private bool _isLeftPressed;
    private bool _isRightPressed;
    private void Update()
    {
        ManageInputs();
    }

    private void ManageInputs()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnLeftPressed();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            OnLeftReleased();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnRightPressed();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            OnRightReleased();
        }
        
        playerController.ManageMovement(_isLeftPressed, _isRightPressed);
    }
    public void OnRightPressed()
    {
        _isRightPressed = true;
    }
    public void OnRightReleased()
    {
        _isRightPressed = false;
    }
    public void OnLeftPressed()
    {
        _isLeftPressed = true;
    }
    public void OnLeftReleased()
    {
        _isLeftPressed = false;
    }
}
