using UnityEngine;

namespace Plates
{
    public interface IPlate
    {
        float GetHeight();
        float GetYPos();
        void StackMeTo(Transform transformToStackTo, float yOffset);
        bool IsStacked();
    }
}