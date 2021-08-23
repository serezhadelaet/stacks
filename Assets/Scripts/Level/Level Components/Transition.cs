using System.Collections.Generic;
using Movement;
using UnityEngine;

public class Transition : BaseTrigger, ICanTransitMovable
{
    [SerializeField] private List<Transform> points = null;
    [SerializeField] private Transition oppositeTransition = null;
    [SerializeField] private bool backwards = false;
    
    private IMovable _movable;
    private Transform _transformToMove;
    private float _speed;
    private bool _canUse = true;
    private Vector3 _nextPos;
    private int _currentIndex;
    private bool _isMoving;
    
    private const float MinDistance = 0.0001f;

    private void Awake()
    {
        if (backwards)
            points.Reverse();
    }

    private void Update()
    {
        if (!_isMoving) 
            return;
        
        _transformToMove.position = Vector3.MoveTowards(_transformToMove.position, _nextPos, Time.deltaTime * _speed);
        var distance = Vector3.Distance(_transformToMove.position, _nextPos);
        if (distance > MinDistance) 
            return;

        _transformToMove.position = _transformToMove.position;
        
        _currentIndex++;
        
        if (_currentIndex > points.Count - 1)
        {
            OnTransitionEnd();
            return;
        }
                
        _nextPos = points[_currentIndex].position;
        _nextPos.y = _transformToMove.transform.position.y;
    }

    private void OnTransitionEnd()
    {
        _isMoving = false;
        _movable.AutoMove(backwards ? -Vector3.forward : Vector3.forward);
    }

    protected override void OnEntered(Collider other)
    {
        if (!_canUse)
            return;
        
        var movable = other.GetComponent<IMovable>();
        if (movable == null) 
            return;

        if (!movable.IsMoving())
            return;

        SetupMovable(movable);
    }

    protected override void OnLeave(Collider other)
    {
        SetCanUse(true);
    }

    public void SetupMovable(IMovable movable)
    {
        _movable = movable;

        oppositeTransition.SetCanUse(false);
        movable.SetCanMove(false);
        MoveAlong(movable.GetTransform(), movable.GetSpeed());
    }

    public void SetCanUse(bool f)
    {
        _canUse = f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        Gizmos.DrawCube(transform.position, Vector3.one);
        Gizmos.color = Color.white;
    }

    private void MoveAlong(Transform transformToMove, float speed)
    {
        _transformToMove = transformToMove;
        _speed = speed;
        _currentIndex = 0;
        _nextPos = points[_currentIndex].position;
        _nextPos.y = _transformToMove.transform.position.y;
        _isMoving = true;
    }
}