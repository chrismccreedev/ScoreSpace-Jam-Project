using UnityEngine;

namespace ScoreSpace
{
    public class BotSpawner : Spawner
    {
        [SerializeField] private Bot[] _botPrefabs;
        [SerializeField] private Player _player;

        public Bot SpawnedBot { get; private set; }

        protected override void StartRandomSpawn()
        {
            Bot prefab = _botPrefabs[Random.Range(0, _botPrefabs.Length - 1)];
            Vector2 position = GetRandomPosition();

            Bot bot = Instantiate(prefab, position, Quaternion.identity, Parent);
            bot.InitializeBot(_player);
            SpawnedBot = bot;
        }
    }
}
