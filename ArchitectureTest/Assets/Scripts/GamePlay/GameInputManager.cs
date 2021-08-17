using UnityEngine;

namespace GamePlay
{
    public class GameInputManager : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GamePlayView gamePlayView;
        [SerializeField] private PlayerController playerController;
    
        private bool _isLeftPressed;
        private bool _isRightPressed;

        private bool _isUsingMobileControllers;
        private void FixedUpdate()
        {
            ManageInputs();
        }

        private void ManageInputs()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _isUsingMobileControllers = false;
            }
        
            if (_isUsingMobileControllers)
            {
                playerController.ManageMovement(_isLeftPressed, _isRightPressed);
                return;
            }
        
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _isLeftPressed = true;
                _isRightPressed = false;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _isRightPressed = true;
                _isLeftPressed = false;
            }
            else
            {
                _isLeftPressed = false;
                _isRightPressed = false;
            }
        
            playerController.ManageMovement(_isLeftPressed, _isRightPressed);
        
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Q))
            {
                gamePlayView.OnDebugLose();
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                gamePlayView.OnDebugWin();
            }
#endif
        }
        public void OnRightPressed()
        {
            _isUsingMobileControllers = true;
            _isRightPressed = true;
        }
        public void OnRightReleased()
        {
            _isRightPressed = false;
        }
        public void OnLeftPressed()
        {
            _isUsingMobileControllers = true;
            _isLeftPressed = true;
        }
        public void OnLeftReleased()
        {
            _isLeftPressed = false;
        }
    }
}
