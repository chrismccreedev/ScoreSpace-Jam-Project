using System;
using UnityEngine;

namespace ScoreSpace
{
    public class CollisionFinder : MonoBehaviour, IFinder
    {
        public event Action<LiveObject> OnLiveObjectEnters;
        public event Action<LiveObject> OnLiveObjectExits;

        public event Action<Rigidbody2D> OnRigidbodyEnters;
        public event Action<Rigidbody2D> OnRigidbodyExits;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out LiveObject liveObject))
                OnLiveObjectEnters?.Invoke(liveObject);

            if (collision.transform.TryGetComponent(out Rigidbody2D rigidbody))
                OnRigidbodyEnters?.Invoke(rigidbody);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out LiveObject liveObject))
                OnLiveObjectExits?.Invoke(liveObject);

            if (collision.transform.TryGetComponent(out Rigidbody2D rigidbody))
                OnRigidbodyExits?.Invoke(rigidbody);
        }
    }
}
