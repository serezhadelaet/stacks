using System;

namespace Plates
{
    public interface ICanStackPlate : ICanLosePlate
    {
        void StackPlate(IPlate plate);
        int GetPlatesAmount();
        event Action OnLostLastPlate;
        event Action OnLost;
        event Action OnStacked;
    }
}