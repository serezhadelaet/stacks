using Movement;
using Player;
using UnityEngine;

public class Finish : BaseTrigger
{
    [SerializeField] private Collider coll = null;
  
    protected override void OnEntered(Collider other)
    {
        var movable = other.GetComponent<IMovable>();
        if (movable == null) 
            return;

        var player = other.GetComponent<IPlayer>();
        if (player == null)
            return;
        
        coll.enabled = false;
        
        movable.SetMoving(false);
        movable.SetCanMove(true);
        movable.Move(Vector3.forward);
        player.SetFinished();
    }
}