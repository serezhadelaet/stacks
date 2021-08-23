using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayersFactory : PlaceholderFactory<GameObject, BasePlayer>
    {
        public override BasePlayer Create(GameObject prefab)
        {
            var player = base.Create(prefab);
            return player;
        }
    }
}