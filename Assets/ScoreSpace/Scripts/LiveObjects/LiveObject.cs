using System.Collections;
using UnityEngine;

namespace ScoreSpace
{
    public abstract class LiveObject : MonoBehaviour
    {
        [SerializeField] protected Weapon Weapon;
        [SerializeField] protected Rigidbody2D Rigidbody;

        [SerializeField] protected float DestroyDelay = 5;

        [SerializeField] protected float Speed = 4;
        [SerializeField] protected float MaxVelocity = 4;

        protected float StandardSpeedMultiply = 30;

        [SerializeField] protected Team Team;

        public Team GetTeam() => Team;

        private void Awake()
        {
            Weapon.OnEnemyEnters += Attack;
        }

        private void OnDestroy()
        {
            Weapon.OnEnemyEnters -= Attack;
        }

        protected void Attack(LiveObject liveObject)
        {
            liveObject.Destroy();
        }

        protected void Destroy()
        {
            enabled = false;
            StartCoroutine(WaitForDestroy());
        }

        private IEnumerator WaitForDestroy()
        {
            yield return new WaitForSeconds(DestroyDelay);
            Destroy(gameObject);
        }

        protected virtual void Move()
        {
            Rigidbody.AddForce(Speed * StandardSpeedMultiply * Time.deltaTime * transform.up);
            Rigidbody.velocity = new Vector2(Mathf.Clamp(Rigidbody.velocity.x, -MaxVelocity, MaxVelocity), Mathf.Clamp(Rigidbody.velocity.y, -MaxVelocity, MaxVelocity));
        }
    }

    public enum Team
    {
        Player,
        Bot
    }
}
