using System.Collections.Generic;
using UnityEngine;

namespace ScoreSpace
{
    public class BotManager : MonoBehaviour
    {
        [SerializeField] private BotSpawner _spawner;

        private List<Bot> _bots = new(10);

        public IEnumerable<Bot> Bots => _bots;

        private void Awake()
        {
            _spawner.OnObjectSpawned += AddBot;
        }

        private void OnDestroy()
        {
            _spawner.OnObjectSpawned -= AddBot;
        }

        private void AddBot()
        {
            if (!_bots.Contains(_spawner.SpawnedBot))
            {
                _spawner.SpawnedBot.OnObjectDestroyed += RemoveBot;
                _bots.Add(_spawner.SpawnedBot);
            }
        }

        private void RemoveBot(LiveObject liveObject)
        {
            liveObject.OnObjectDestroyed -= RemoveBot;
            _bots.Remove((Bot)liveObject);
        }
    }
}
