using UnityEngine;

namespace Plates
{
    public class Stackable : MonoBehaviour, IStackable
    {
        private bool _stacked;
    
        public void StackTo(Transform transformToStackTo, float yOffset)
        {
            _stacked = true;
        
            transform.SetParent(transformToStackTo);
            transform.localPosition = Vector3.up * yOffset;
            transform.localEulerAngles = Vector3.zero;
        }

        public bool HasStacked() => _stacked;
    }
}