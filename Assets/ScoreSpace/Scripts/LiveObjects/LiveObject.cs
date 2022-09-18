using System;
using System.Collections;
using UnityEngine;

namespace ScoreSpace
{
    public abstract class LiveObject : MonoBehaviour
    {
        public event Action<LiveObject> OnObjectDestroyed;

        [SerializeField] protected CollisionFinder Finder;
        [SerializeField] protected Rigidbody2D Rigidbody;

        [SerializeField] protected float DestroyDelay = 5;

        [SerializeField] private float _speed = 4;
        [SerializeField] private float _maxVelocity = 4;

        protected float StandardSpeedMultiply = 30;

        [SerializeField] protected Team Team;

        public float Speed { get => _speed; set => _speed = value; }
        public float MaxVelocity { get => _maxVelocity; set => _maxVelocity = value; }

        public Team GetTeam() => Team;

        private void Awake()
        {
            Finder.OnLiveObjectEnters += Attack;
        }

        private void OnDestroy()
        {
            Finder.OnLiveObjectEnters -= Attack;
        }

        protected virtual void Attack(LiveObject liveObject)
        {
            liveObject.Destroy();
        }

        public virtual void Destroy()
        {
            enabled = false;
            StartCoroutine(WaitForDestroy());
        }

        private IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(DestroyDelay);
            OnObjectDestroyed?.Invoke(this);
            Destroy(gameObject);
        }

        protected virtual void Move()
        {
            Rigidbody.AddForce(_speed * StandardSpeedMultiply * Time.deltaTime * transform.up);
            Rigidbody.velocity = new Vector2(Mathf.Clamp(Rigidbody.velocity.x, -_maxVelocity, _maxVelocity), Mathf.Clamp(Rigidbody.velocity.y, -_maxVelocity, _maxVelocity));
        }
    }

    public enum Team
    {
        Player,
        Bot
    }
}
