using System;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using Player;
using UnityEngine;

namespace Plates
{
    public class PlatesStacker : MonoBehaviour, ICanStackPlate
    {
        [SerializeField] private float platesOffset = 0.07f;
        [SerializeField] private Transform platesHolderTr = null;
    
        private readonly List<IPlate> _plates = new List<IPlate>(50);
        private IPlayer _player;
        private float _currentPlatesYOffset;
    
        public int GetPlatesAmount() => _plates.Count;

        public event Action OnLostLastPlate;
        public event Action OnLost;
        public event Action OnStacked;
    
        private void Awake()
        {
            _player = GetComponent<IPlayer>();
        }

        public void LosePlate(Placement initiator)
        {
            var amount = GetPlatesAmount();
            if (amount == 0)
            {
                OnLostLastPlate?.Invoke();
                return;
            }
        
            var lastPlate = _plates[amount - 1];
            var heightWithOffset = lastPlate.GetHeight() / 2 + lastPlate.GetYPos() + platesOffset;
        
            _player.GetModelTransform().localPosition -= Vector3.up * heightWithOffset;
        
            _currentPlatesYOffset -= heightWithOffset;
        
            lastPlate.StackMeTo(initiator.transform, lastPlate.GetYPos());
        
            _plates.Remove(lastPlate);
            Vibrate();
            OnLost?.Invoke();
        }
    
        public void StackPlate(IPlate plate)
        {
            var heightWithOffset = plate.GetHeight() / 2 + plate.GetYPos() + platesOffset;
        
            _player.GetModelTransform().localPosition += Vector3.up * heightWithOffset;

            var amount = GetPlatesAmount();

            if (_currentPlatesYOffset == default)
                _currentPlatesYOffset = plate.GetYPos();
        
            if (_plates.Count > 0)
            {
                var prevPlate = _plates[amount - 1];
        
                _currentPlatesYOffset += prevPlate.GetHeight() / 2 + plate.GetYPos() + platesOffset;
            }
        
            plate.StackMeTo(platesHolderTr, _currentPlatesYOffset);
        
            _plates.Add(plate);
            Vibrate();
            OnStacked?.Invoke();
        }
    
        private void Vibrate()
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }
    }
}