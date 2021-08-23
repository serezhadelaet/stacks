using UnityEngine;

public abstract class BaseTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayerMask = default;
    [SerializeField] private bool isMultiplyUsing = false;

    private bool _hasTriggered;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_hasTriggered && !isMultiplyUsing)
            return;

        if (IsTargetLayer(other))
        {
            _hasTriggered = true;
            OnEntered(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsTargetLayer(other))
        {
            OnLeave(other);
        }
    }

    private bool IsTargetLayer(Collider other) => targetLayerMask == (targetLayerMask | (1 << other.gameObject.layer));
    
    protected abstract void OnEntered(Collider other);

    protected virtual void OnLeave(Collider other)
    {
        
    }
}