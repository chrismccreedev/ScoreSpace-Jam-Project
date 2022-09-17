using System;
using UnityEngine;

namespace ScoreSpace
{
    public class CollisionFinder : MonoBehaviour, IFinder
    {
        public event Action<LiveObject> OnLiveObjectEnters;
        public event Action<LiveObject> OnLiveObjectExits;

        [SerializeField] private Team _enemyTeam;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out LiveObject liveObject) && liveObject.GetTeam() == _enemyTeam)
                OnLiveObjectEnters?.Invoke(liveObject);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out LiveObject liveObject) && liveObject.GetTeam() == _enemyTeam)
                OnLiveObjectExits?.Invoke(liveObject);
        }
    }
}
