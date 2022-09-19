using UnityEngine;

namespace ScoreSpace
{
    public class PlayerWeaponFinder : CollisionFinder
    {
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out Player player))
                return;

            base.OnCollisionEnter2D(collision);
        }

        protected override void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out Player player))
                return;

            base.OnCollisionExit2D(collision);
        }
    }
}
