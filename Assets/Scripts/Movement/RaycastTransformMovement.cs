using UnityEngine;

namespace Movement
{
    public class RaycastTransformMovement : MonoBehaviour, IMovable
    {
        [SerializeField] private LayerMask movingLayers = default;
        [SerializeField] private LayerMask transitionLayer = default;
        [SerializeField] private Transform rayCastTr = null;
        [SerializeField] private float movementSpeed = 20;
    
        private bool _isMoving;
        private bool _canMove = true;
        private Vector3 _destinationPos;
    
        public void Move(Vector3 dir)
        {
            if (_isMoving || !_canMove)
                return;
        
            var closest = GetClosestPointByDirection(dir);
            if (closest == default)
                return;
        
            _destinationPos = closest;
            _isMoving = true;
        }

        private void Update()
        {
            if (!_canMove)
            {
                _destinationPos = default;
            }
            if (_isMoving)
            {
                MoveToDestinationPos();
            
                if (HasArriveToDestinationPos())
                {
                    transform.position = _destinationPos;
                    _isMoving = false;
                }
            }
        }

        public float GetSpeed() => movementSpeed;
        public bool IsMoving() => _isMoving;
        public bool CanMove() => _canMove;
        public void SetCanMove(bool f) => _canMove = f;
        public void SetMoving(bool f) => _isMoving = f;
        public Transform GetTransform() => transform;

        public void AutoMove(Vector3 dir)
        {
            SetMoving(false);
            SetCanMove(true);
            Move(dir);
        }
    
        private void MoveToDestinationPos()
        {
            if (_destinationPos == default)
                return;
            transform.position = Vector3.MoveTowards(transform.position, _destinationPos, Time.deltaTime * movementSpeed);
        }

        private bool HasArriveToDestinationPos() => Vector3.Distance(transform.position, _destinationPos) < 0.001f;
    
        private Vector3 GetClosestPointByDirection(Vector3 dir)
        {
            if (!Physics.Raycast(rayCastTr.position, dir, out var hit, 500, movingLayers))
                return default;
        
            var isTransition = IsTransitionLayer(hit.transform.gameObject.layer);
            var offset = isTransition ? dir / 2 : -dir / 2;
            return new Vector3(hit.point.x, transform.position.y, hit.point.z) + offset;
        }
    
        private bool IsTransitionLayer(int layer) => transitionLayer == (transitionLayer | (1 << layer));
    }
}