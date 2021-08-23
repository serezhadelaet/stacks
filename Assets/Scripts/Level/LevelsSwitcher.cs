using UI;
using UnityEngine;
using Zenject;

namespace Level
{
    public class LevelsSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject[] levelsPrefabs = null;
    
        private LevelsFactory _levelsFactory;
        private LoseOverlay _loseOverlay;
        private WinOverlay _winOverlay;
        private GameOverlay _gameOverlay;

        private int _currentIndex;
        private global::Level.Level _currentLevel;
    
        [Inject]
        private void Construct(GameOverlay gameOverlay, LevelsFactory levelsFactory, LoseOverlay loseOverlay, WinOverlay winOverlay)
        {
            _levelsFactory = levelsFactory;
            _winOverlay = winOverlay;
            _loseOverlay = loseOverlay;
            _gameOverlay = gameOverlay;
        
            SubscribeForUiEvents();
        }
    
        private void Start()
        {
            RestartLevel();
        }

        private void SubscribeForUiEvents()
        {
            _loseOverlay.OnContinueClicked += RestartLevel;
            _winOverlay.OnContinueClicked += NextLevel;
        }

        private void OnDestroy()
        {
            _loseOverlay.OnContinueClicked -= RestartLevel;
            _winOverlay.OnContinueClicked -= NextLevel;
        }

        private void RestartLevel()
        {
            CloseCurrentLevel();
            StartLevel();
        }

        private void NextLevel()
        {
            CloseCurrentLevel();
            _currentIndex++;
            StartLevel();
        }

        private void StartLevel()
        {
            _gameOverlay.UpdateLevel(_currentIndex + 1);
            _currentLevel = _levelsFactory.Create(GetLevelByIndex(_currentIndex));
        }

        private void CloseCurrentLevel()
        {
            if (_currentLevel)
                Destroy(_currentLevel.gameObject);
        }

        private GameObject GetLevelByIndex(int index)
        {
            if (index < 0 || index > levelsPrefabs.Length - 1)
                index = 0;
            return levelsPrefabs[index];
        }
    }
}