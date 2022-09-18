using UnityEngine;

namespace ScoreSpace
{
    public class BonusSpawner : Spawner
    {
        [SerializeField] private Bonus[] _bonusPrefabs;

        protected override void StartRandomSpawn()
        {
            Bonus prefab = _bonusPrefabs[Random.Range(0, _bonusPrefabs.Length)];
            Vector2 position = GetRandomPosition();

            Instantiate(prefab, position, Quaternion.identity, Parent);
        }
    }
}
