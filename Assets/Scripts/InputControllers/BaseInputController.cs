using UnityEngine;

namespace InputControllers
{
    public abstract class BaseInputController : MonoBehaviour, IInputController
    {
        protected Vector3 InputDirection;

        public virtual bool IsInputUpdated() => InputDirection != default;
        public virtual Vector3 GetInputDirection() => InputDirection;
    }
}