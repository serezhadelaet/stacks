using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOverlay : BaseOverlay
    {
        [SerializeField] private TextMeshProUGUI scoreText = null;
        [SerializeField] private TextMeshProUGUI levelText = null;
        [SerializeField] private Image progressBarFillImage = null;

        private void Awake()
        {
            ResetScore();
        } 

        public void UpdateScore(int i)
        {
            scoreText.text = i.ToString();
        }
        
        public void UpdateLevel(int i)
        {
            ResetScore();
            levelText.text = i.ToString();
        }
        
        public void UpdateDistance(float value)
        {
            progressBarFillImage.fillAmount = value;
        }
        
        private void ResetScore() => UpdateScore(0);
    }
}