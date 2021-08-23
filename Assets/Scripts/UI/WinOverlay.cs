using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinOverlay : BaseOverlay
    {
        [SerializeField] private Button continueButton = null;
        [SerializeField] private Image banner = null;

        public event Action OnContinueClicked;

        private Vector3 _initialBannerPos;
        private Vector3 _initialContinuePos;
        
        private void Start()
        {
            _initialBannerPos = banner.transform.localPosition;
            _initialContinuePos = continueButton.transform.localPosition;
            
            banner.transform.localPosition = Vector3.up * 1500;
            continueButton.transform.localPosition = Vector3.up * -1500;
            
            continueButton.onClick.AddListener(OnButtonContinueClicked);
        }
        
        private void OnButtonContinueClicked() {
        {
            Hide();
            OnContinueClicked?.Invoke();
        }}

        protected override void OnShown()
        {
            base.OnShown();
            AnimatedShow();
        }

        private void AnimatedShow()
        {
            banner.transform.DOLocalMove(_initialBannerPos, 1f)
                .SetEase(Ease.InOutElastic)
                .SetUpdate(true);
            
            continueButton.transform.DOLocalMove(_initialContinuePos, 1f)
                .SetEase(Ease.InOutElastic)
                .SetUpdate(true);
        }
    }
}