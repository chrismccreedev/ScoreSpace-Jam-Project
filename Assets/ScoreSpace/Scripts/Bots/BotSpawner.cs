using System.Collections;
using UnityEngine;

namespace ScoreSpace
{
    public class BotSpawner : MonoBehaviour
    {
        [SerializeField] private Vector2[] _spawnPositions;
        [SerializeField] private Bot[] _botPrefabs;

        [SerializeField] private float _spawnDelay;
        [SerializeField] private float _spawnDelayReduction;

        [SerializeField] private Transform _parent;
        [SerializeField] private Player _player;

        private float _currentDelay;

        private void Awake()
        {
            _currentDelay = _spawnDelay;
        }

        private void Start()
        {
            StartSpawn();
        }

        public void StartSpawn()
        {
            StartCoroutine(WaitForSpawn());
        }

        public void StopSpawn()
        {
            StopAllCoroutines();
        }

        private IEnumerator WaitForSpawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(_currentDelay);

                _currentDelay -= _spawnDelayReduction;
                StartRandomSpawn();
            }
        }

        private void StartRandomSpawn()
        {
            Bot prefab = _botPrefabs[Random.Range(0, _botPrefabs.Length - 1)];
            Vector2 position = _spawnPositions[Random.Range(0, _spawnPositions.Length - 1)];

            Bot bot = Instantiate(prefab, position, Quaternion.identity, _parent);
            bot.InitializeBot(_player);
        }
    }
}
