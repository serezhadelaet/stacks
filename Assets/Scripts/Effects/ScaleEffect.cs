using DG.Tweening;
using UnityEngine;

namespace Effects
{
    public class ScaleEffect : MonoBehaviour, IInteractEffect
    {
        [SerializeField] private Transform transformToScale = null;
        [SerializeField] private Vector3 scaleTo = default;
        [SerializeField] private float duration = .5f;

        private Tween _tween;
        private Vector3 _initialScale;
    
        public void DoEffect()
        {
            if (_initialScale == default)
                _initialScale = transformToScale.localScale;

            _tween?.Kill();
            transformToScale.localScale = _initialScale;
        
            _tween = transformToScale.DOScale(scaleTo, duration)
                .SetEase(Ease.InOutQuad)
                .SetLoops(2, LoopType.Yoyo);
        }
    }
}