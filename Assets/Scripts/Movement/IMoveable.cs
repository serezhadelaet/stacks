using UnityEngine;

namespace Movement
{
    public interface IMovable
    {
        float GetSpeed();
        void Move(Vector3 dir);
        bool IsMoving();
        bool CanMove();
        void SetCanMove(bool f);
        void SetMoving(bool f);
        void AutoMove(Vector3 dir);
        Transform GetTransform();
    }
}