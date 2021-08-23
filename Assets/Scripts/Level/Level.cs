using DG.Tweening;
using Player;
using UI;
using UnityEngine;
using Zenject;

namespace Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Finish finish = null;
        [SerializeField] private PlayersSpawner playersSpawner = null;
        [SerializeField] private LevelCameras levelCameras = null;
    
        private GameOverlay _gameOverlay;
        private LoseOverlay _loseOverlay;
        private WinOverlay _winOverlay;
        private float _maxDistance;
        private IPlayer _player;
    
        [Inject]
        private void Construct(GameOverlay gameOverlay, LoseOverlay loseOverlay, WinOverlay winOverlay)
        {
            _gameOverlay = gameOverlay;
            _loseOverlay = loseOverlay;
            _winOverlay = winOverlay;
        }
    
        private void Awake()
        {
            Time.timeScale = 1;
        
            SpawnPlayer();
            SetupLevel();
        
            _gameOverlay.Show();
        }

        private void SpawnPlayer()
        {
            _player = playersSpawner.SpawnPlayer();
        }

        private void SetupLevel()
        {
            _maxDistance = finish.transform.position.z - _player.GetPositionZ();
            _player.OnLose += OnLose;
            _player.OnWin += OnWin;
            levelCameras.BindCamerasTo(_player.GetTransform(), _player.GetModelTransform());
        }

        private void OnDestroy()
        {
            _player.OnLose -= OnLose;
            _player.OnWin -= OnWin;
        }

        private void OnWin()
        {
            levelCameras.EnableFinishCamera();
            _gameOverlay.Hide();
            _winOverlay.Show();
        }
    
        private void OnLose()
        {
            SlowTime();
            _gameOverlay.Hide();
            _loseOverlay.Show();
        }

        private void SlowTime()
        {
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, 0.25f)
                .SetUpdate(true);
        }

        private void Update()
        {
            var currentDistance = finish.transform.position.z - _player.GetPositionZ();
            var value = 1 - (currentDistance / _maxDistance);
            _gameOverlay.UpdateDistance(value);
        }
    }
}