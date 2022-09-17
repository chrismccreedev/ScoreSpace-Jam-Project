using System;
using UnityEngine;

namespace ScoreSpace
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        public event Action<LiveObject> OnEnemyEnters;
        public event Action<LiveObject> OnEnemyExits;

        [SerializeField] private Team _enemyTeam;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out LiveObject enemy) && enemy.GetTeam() == _enemyTeam)
                OnEnemyEnters?.Invoke(enemy);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out LiveObject enemy) && enemy.GetTeam() == _enemyTeam)
                OnEnemyExits?.Invoke(enemy);
        }
    }
}
