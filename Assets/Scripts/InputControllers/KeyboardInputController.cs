using UnityEngine;

namespace InputControllers
{
    public class KeyboardInputController : BaseInputController
    {
        private void Update()
        {
            InputDirection = default;
        
            if (Input.GetKeyDown(KeyCode.W))
                InputDirection = Vector3.forward;
        
            if (Input.GetKeyDown(KeyCode.D))
                InputDirection = Vector3.right;
        
            if (Input.GetKeyDown(KeyCode.A))
                InputDirection = Vector3.left;
        
            if (Input.GetKeyDown(KeyCode.S))
                InputDirection = Vector3.back;
        }
    }
}