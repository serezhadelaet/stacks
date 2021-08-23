using System;
using DG.Tweening;
using InputControllers;
using MoreMountains.NiceVibrations;
using Movement;
using Plates;
using UI;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(ICanStackPlate))]
    [RequireComponent(typeof(IInputController))]
    [RequireComponent(typeof(IMovable))]
    public class BasePlayer : MonoBehaviour, IPlayer
    {
        [SerializeField] private LayerMask platesLayer = default;
        [SerializeField] private Transform personModelTr = null;
        [SerializeField] private Transform activeObjectsTr = null;
        [SerializeField] private PlayerAnimation playerAnimation = null;
        [SerializeField] private int maxPlatesToAllowShakeEffect = 20;
    
        private bool _isFinished;
        private int _score;
        private GameOverlay _gameOverlay;
        private GuideOverlay _guideOverlay;
        private ICanStackPlate _plateStacker;
        private IInputController _inputController;
        private IMovable _playerMovement;
    
        public event Action OnLose;
        public event Action OnWin;
        public Transform GetTransform() => transform;
        public float GetPositionZ() => transform.position.z;
        public Transform GetModelTransform() => personModelTr;
    
        [Inject]
        private void Construct(GameOverlay gameOverlay, GuideOverlay guideOverlay)
        {
            _gameOverlay = gameOverlay;
            _guideOverlay = guideOverlay;
        }
    
        private void Awake()
        {
            _playerMovement = GetComponent<IMovable>();
            _inputController = GetComponent<IInputController>();
            _plateStacker = GetComponent<ICanStackPlate>();
            _plateStacker.OnLost += Jump;
            _plateStacker.OnStacked += OnStacked;
            _plateStacker.OnLostLastPlate += OnLostLastPlate;
        
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }
    
        private void OnDestroy()
        {
            _plateStacker.OnLost -= Jump;
            _plateStacker.OnStacked -= OnStacked;
            _plateStacker.OnLostLastPlate -= OnLostLastPlate;
        }

        private void Jump()
        {
            playerAnimation.Jump();
        }
    
        private void OnLostLastPlate()
        {
            _playerMovement.SetMoving(false);
            _playerMovement.SetCanMove(false);
            OnLose?.Invoke();
        }
    
        private void OnStacked()
        {
            _score++;
            _gameOverlay.UpdateScore(_score);
            Jump();
        }

        public void SetFinished()
        {
            _isFinished = true;
        }
    
        private void Update()
        {
            if (!_playerMovement.IsMoving())
            {
                if (_isFinished)
                {
                    OnWin?.Invoke();
                    playerAnimation.Finished();
                    _isFinished = false;
                }
            }
        
            if (!_playerMovement.CanMove())
                return;
        
            if (_inputController.IsInputUpdated())
            {
                _guideOverlay.Hide();
            
                MMVibrationManager.Haptic(HapticTypes.LightImpact);

                var dir = _inputController.GetInputDirection();
                
                _playerMovement.Move(dir);
                RotateObjects(dir * 5);
            }
        }

        private void RotateObjects(Vector3 dir)
        {
            // Here we are just swapping direction x <> z to get a proper shake effect
            if (Math.Abs(dir.x) > float.Epsilon)
            {
                dir.z = -dir.x;
                dir.x = 0;
            }
            else if (Math.Abs(dir.z) > float.Epsilon)
            {
                dir.x = dir.z;
                dir.z = 0;
            }
        
            if (_plateStacker.GetPlatesAmount() > maxPlatesToAllowShakeEffect)
                return;
        
            float delay = 0;
            foreach (Transform tr in activeObjectsTr)
            {
                delay += 0.025f;
                DOTween.Kill(tr);
                tr.localEulerAngles = Vector3.zero;
                tr.DOLocalRotate(dir, 0.1f)
                    .SetEase(Ease.InOutQuad)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetDelay(delay);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsPlatesLayer(other)) 
                return;
        
            var plate = other.GetComponent<IPlate>();
            if (plate != null && !plate.IsStacked())
                _plateStacker.StackPlate(plate);
        }
    
        private bool IsPlatesLayer(Collider other) => (platesLayer == (platesLayer | (1 << other.gameObject.layer)));
    }
}
