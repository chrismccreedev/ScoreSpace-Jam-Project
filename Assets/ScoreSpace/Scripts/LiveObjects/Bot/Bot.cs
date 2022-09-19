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

        public override void Destroy()
        {
            if (!_isDestroyed)
                Score.Add(1);
            base.Destroy();
            GetComponent<SpriteRenderer>().color = new Color32(155, 155, 155, 255);
        }

        protected override void Move()
        {
            base.Move();
            RotateToPlayer();
        }

        private void RotateToPlayer()
        {
            if (_player == null)
                return;

            Vector3 heading = _player.transform.position - transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            float z = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Rigidbody.SetRotation(z);
        }
    }
}
