using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayersSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint = null;
        [SerializeField] private GameObject playerPrefab = null;

        private PlayersFactory _playersFactory;
    
        [Inject]
        private void Construct(PlayersFactory playersFactory)
        {
            _playersFactory = playersFactory;
        }

        public IPlayer SpawnPlayer()
        {
            var player = _playersFactory.Create(playerPrefab);
            player.transform.SetParent(spawnPoint, false);
            return player;
        }
    }
}