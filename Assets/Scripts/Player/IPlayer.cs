using System;
using UnityEngine;

namespace Player
{
    public interface IPlayer
    {
        void SetFinished();
        event Action OnWin;
        event Action OnLose;
        float GetPositionZ();
        Transform GetTransform();
        Transform GetModelTransform();
    }
}