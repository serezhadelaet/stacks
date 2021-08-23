using UnityEngine;

namespace Plates
{
    public class StackableFlying : MonoBehaviour, IStackable
    {
        [SerializeField] private float moveSpeed = 1f;

        private bool _stacked;
        private Transform _transformToStackTo;
        private float _yOffset;
        private float _timeSinceStartFloat;
        private bool _shouldMove;
        private Vector3 _startPos;

        public void StackTo(Transform transformToStackTo, float yOffset)
        {
            _timeSinceStartFloat = 0;
            _stacked = true;
            _shouldMove = true;
            _transformToStackTo = transformToStackTo;
            _yOffset = yOffset;
            _startPos = transform.position;
        }

        private void Update()
        {
            if (_shouldMove)
            {
                var targetPos = _transformToStackTo.position;
                targetPos.y = _yOffset;
            
                if (Vector3.Distance(transform.position, targetPos) < 0.001f)
                {
                    StickToTargetTransform();
                    return;
                }
            
                _timeSinceStartFloat += Time.deltaTime * moveSpeed;
                transform.position = Vector3.Lerp(_startPos, targetPos, _timeSinceStartFloat);
            }
        }

        private void StickToTargetTransform()
        {
            _shouldMove = false;
            transform.SetParent(_transformToStackTo);
            transform.localPosition = Vector3.up * _yOffset;
            transform.localEulerAngles = Vector3.zero;
        }
    
        public bool HasStacked() => _stacked;
    }
}