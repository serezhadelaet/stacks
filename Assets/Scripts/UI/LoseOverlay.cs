using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoseOverlay : BaseOverlay
    {
        [SerializeField] private Button continueButton = null;

        public event Action OnContinueClicked;
        
        private void Start()
        {
            continueButton.onClick.AddListener(OnButtonContinueClicked);
        }
        
        private void OnButtonContinueClicked() {
        {
            Hide();
            OnContinueClicked?.Invoke();
        }}
    }
}