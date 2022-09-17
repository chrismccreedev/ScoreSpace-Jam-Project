using UnityEngine;

namespace ScoreSpace
{
    public class Bot : LiveObject
    {
        private Player _player;

        public void InitializeBot(Player player)
        {
            _player = player;
        }

        private void Update()
        {
            Move();
        }

        protected override void Move()
        {
            base.Move();
            RotateToPlayer();
        }

        private void RotateToPlayer()
        {
            Vector3 heading = _player.transform.position - transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            float z = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Rigidbody.SetRotation(z);
        }
    }
}
