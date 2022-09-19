using UnityEngine;

namespace ScoreSpace
{
    public class BonusSpawner : Spawner
    {
        [SerializeField] private Bonus[] _bonusPrefabs;

        public Bonus SpawnedBonus { get; private set; }

        protected override void StartRandomSpawn()
        {
            Bonus prefab = _bonusPrefabs[Random.Range(0, _bonusPrefabs.Length)];
            Vector2 position = GetRandomPosition();

            SpawnedBonus = Instantiate(prefab, position, Quaternion.identity, Parent);
        }
    }
}
