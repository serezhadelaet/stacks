using UnityEngine;

namespace Movement
{
    public class RaycastTransformFlyingMovement : RaycastTransformMovement
    {
        [SerializeField] private Transform transformToFloat = null;
        [SerializeField] private float flyingRange = 2;
        [SerializeField] private float flyingSpeed = 3;
    
        private void LateUpdate()
        {
            var pos = transformToFloat.localPosition;
            pos.y = flyingRange + (Mathf.Sin(Time.time * flyingSpeed) * flyingRange);
            transformToFloat.localPosition = pos;
        }
    }
}