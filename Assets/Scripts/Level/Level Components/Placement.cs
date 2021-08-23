using Plates;
using UnityEngine;

public class Placement : BaseTrigger
{
    protected override void OnEntered(Collider other)
    {
        var plateTaker = other.GetComponent<ICanLosePlate>();
        plateTaker?.LosePlate(this);
    }
}