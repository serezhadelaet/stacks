using UnityEngine;

namespace Plates
{
    public interface IStackable
    {
        void StackTo(Transform transformToStackTo, float yOffset);
        bool HasStacked();
    }
}