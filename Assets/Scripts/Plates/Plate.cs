using UnityEngine;

namespace Plates
{
    public class Plate : MonoBehaviour, IPlate
    {
        private float _yPos;
        private float _yScale;

        private IStackable _stackable;

        private void Awake()
        {
            _stackable = GetComponent<IStackable>();
        
            _yPos = transform.position.y;
            _yScale = transform.localScale.y;
        }
    
        public void StackMeTo(Transform transformToStackTo, float yOffset)
        {
            _stackable?.StackTo(transformToStackTo, yOffset);
        }
    
        public bool IsStacked() => _stackable?.HasStacked() ?? true;
        public float GetHeight() => _yScale;
        public float GetYPos() => _yPos;
    }
}