using UnityEngine;

namespace InputControllers
{
    public class SwipeInputController : BaseInputController
    {
        [SerializeField] private float maxTouchOffset = 50f;
    
        private Vector3 _touchPos;
    
        private void Update()
        {
            InputDirection = default;
        
            if (Input.GetMouseButtonDown(0))
            {
                _touchPos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (Vector3.Distance(_touchPos, Input.mousePosition) < maxTouchOffset) 
                    return;
            
                var side = (Input.mousePosition - _touchPos).normalized;
                
                if (Mathf.Abs(side.x) > Mathf.Abs(side.y))
                    InputDirection = side.x > 0 ? Vector3.right : Vector3.left;
                else
                    InputDirection = side.y > 0 ? Vector3.forward : Vector3.back;
            }
        }
    }
}