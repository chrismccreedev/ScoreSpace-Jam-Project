using System.Collections.Generic;
using UnityEngine;

namespace ScoreSpace
{
    public class BonusManager : MonoBehaviour
    {
        [SerializeField] private BonusSpawner _spawner;

        private List<Bonus> _bonuses = new(10);

        public IEnumerable<Bonus> Bonuses => _bonuses;

        private void Awake()
        {
            _spawner.OnObjectSpawned += AddBonus;
        }

        private void OnDestroy()
        {
            _spawner.OnObjectSpawned -= AddBonus;
        }

        private void AddBonus()
        {
            if (!_bonuses.Contains(_spawner.SpawnedBonus))
            {
                _spawner.SpawnedBonus.OnBonusUsed += RemoveBonus;
                _bonuses.Add(_spawner.SpawnedBonus);
            }
        }

        private void RemoveBonus(Bonus bonus)
        {
            bonus.OnBonusUsed -= RemoveBonus;
            _bonuses.Remove(bonus);
        }
    }
}
