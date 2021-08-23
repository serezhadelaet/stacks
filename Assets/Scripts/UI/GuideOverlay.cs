using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GuideOverlay : BaseOverlay
    {
        [SerializeField] private Image pointerImage = null;
        
        private void Start()
        {
            pointerImage.transform.DOLocalMove(new Vector3(150, pointerImage.transform.localPosition.y, 0), 1)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}