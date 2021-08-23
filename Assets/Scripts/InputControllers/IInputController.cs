using UnityEngine;

namespace InputControllers
{
    public interface IInputController
    {
        bool IsInputUpdated();
        Vector3 GetInputDirection();
    }
}