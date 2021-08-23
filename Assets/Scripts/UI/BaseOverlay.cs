using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace UI
{
    public class BaseOverlay : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = null;
        [Header("Show params")]
        [SerializeField] private float showIn = .5f;
        [SerializeField] private float showDelay = .5f;
        [SerializeField] private Ease showEaseType = Ease.InOutQuad;
        [Header("Hide params")]
        [SerializeField] private float hideIn = .5f;
        [SerializeField] private float hideDelay = .5f;
        [SerializeField] private Ease hideEaseType = Ease.InOutQuad;

        public bool IsShown;

        private Tween _switchTween;
        
        private void Awake()
        {
            if (canvasGroup == null)
                canvasGroup = gameObject.GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
        }

        protected virtual void OnShown()
        {
            
        }
        
        [Button()]
        public void Hide()
        {
            if (!IsShown)
                return;
            
            canvasGroup.blocksRaycasts = false;
            
            _switchTween?.Kill();
            _switchTween = canvasGroup.DOFade(0, hideIn)
                .SetEase(hideEaseType)
                .SetDelay(hideDelay)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    canvasGroup.interactable = false;
                });
            
            IsShown = false;
        }
        
        [Button()]
        public void Show()
        {
            if (IsShown)
                return;
            
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            
            _switchTween?.Kill();
            _switchTween = canvasGroup.DOFade(1, showIn)
                .SetEase(showEaseType)
                .SetUpdate(true)
                .SetDelay(showDelay)
                .OnComplete(OnShown);
            
            IsShown = true;
        }
    }
}