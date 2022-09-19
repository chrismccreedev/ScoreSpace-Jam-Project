using UnityEngine;

namespace ScoreSpace
{
    public class VelocityBonus : Bonus
    {
        [SerializeField] private float _speedGain;
        [SerializeField] private float _maxVelocityGain;
        [SerializeField] private float _rotationSpeedGain;

        protected override void StartBonus()
        {
            Player.GetComponent<TrailRenderer>().enabled = true;
            AddValues(_speedGain, _maxVelocityGain, _rotationSpeedGain);
        }

        protected override void EndBonus()
        {
            Player.GetComponent<TrailRenderer>().enabled = false;
            AddValues(-_speedGain, -_maxVelocityGain, -_rotationSpeedGain);
        }

        private void AddValues(float speed, float maxVelocity, float rotationSpeed)
        {
            Player.Speed += speed;
            Player.MaxVelocity += maxVelocity;
            Player.RotationSpeed += rotationSpeed;
        }
    }
}
