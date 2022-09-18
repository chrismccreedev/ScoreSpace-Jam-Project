using System;
using System.Collections;
using UnityEngine;

namespace ScoreSpace
{
    public abstract class Spawner : MonoBehaviour
    {
        public event Action OnObjectSpawned;

        [SerializeField] protected Vector2[] SpawnPositions;
        [SerializeField] protected Transform Parent;

        [SerializeField] private float _spawnDelay;
        [SerializeField] private float _spawnDelayReduction;
        [SerializeField] private float _minSpawnDelay = 1;

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
                if (_currentDelay < _minSpawnDelay)
                    _currentDelay = _minSpawnDelay;

                StartRandomSpawn();
                OnObjectSpawned?.Invoke();
            }
        }

        protected abstract void StartRandomSpawn();

        protected Vector2 GetRandomPosition() => SpawnPositions[UnityEngine.Random.Range(0, SpawnPositions.Length - 1)];
    }
}
