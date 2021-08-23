using Effects;
using Movement;
using NaughtyAttributes;
using UnityEngine;

public class SideTransition : BaseTrigger, ICanTransitMovable
{
    [Dropdown("GetVectorValues")]
    [SerializeField] private Vector3 sideToMove = Vector3.forward;
    
    private bool _canUse = true;
    private IMovable _movable;
    private IInteractEffect _effect;

    private void Awake()
    {
        _effect = GetComponent<IInteractEffect>();
    }

    public void SetCanUse(bool f)
    {
        _canUse = f;
    }

    public void SetupMovable(IMovable movable)
    {
        _movable.AutoMove(sideToMove);
        _movable = null;
        _effect?.DoEffect();
    }

    protected override void OnEntered(Collider other)
    {
        if (!_canUse)
            return;
        
        _movable = other.GetComponent<IMovable>();
    }

    private void Update()
    {
        if (_movable == null || _movable.IsMoving())
        {
            return;
        }

        SetupMovable(_movable);
    }

    protected override void OnLeave(Collider other)
    {
        SetCanUse(true);
    }
    
    private DropdownList<Vector3> GetVectorValues()
    {
        return new DropdownList<Vector3>()
        {
            { "Right",   Vector3.right },
            { "Left",    Vector3.left },
            { "Forward", Vector3.forward },
            { "Back",    Vector3.back }
        };
    }
}